using API.Core.Controllers;
using API.Identidade.Models;
using API.Identidade.Services;
using Core.Bus;
using Core.Bus.Messages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Identidade.Controllers
{
    [Route("api/identidade")]
    public class AuthController : BaseController
    {
        private readonly AuthenticationService _authenticationService;
        private readonly IMessageBus _bus;

        public AuthController(AuthenticationService authenticationService, IMessageBus bus)
        {
            _authenticationService = authenticationService;
            _bus = bus;
        }

        [HttpPost("new-account")]
        public async Task<ActionResult> Registry(UserRegistryModel userRegistryModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = userRegistryModel.Email,
                Email = userRegistryModel.Email,
                EmailConfirmed = true
            };

            await _bus.RequestAsync<UserMessage, ResponseMessage>(new UserMessage(
                    Guid.NewGuid(),
                    user.Email,
                    userRegistryModel.Cpf
                ));

            return CustomResponse();

            //var result = await _authenticationService.UserManager.CreateAsync(user, userRegistryModel.Password);

            //if (result.Succeeded)
            //{
            //    await _bus.PublishAsync<UserMessage>(new UserMessage(
            //        Guid.NewGuid(),
            //        user.Email,
            //        userRegistryModel.Cpf
            //    ));

            //    return CustomResponse(await _authenticationService.GenerateJwt(userRegistryModel.Email));
            //}
            //else
            //{
            //    foreach (var error in result.Errors)
            //    {
            //        AdicionarErroProcessamento(error.Description);
            //    }

            //    return CustomResponse();
            //}
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginModel userLoginModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _authenticationService.SignInManager.PasswordSignInAsync(userLoginModel.Email,
                userLoginModel.Password,
                false,
                true);

            if (result.Succeeded)
                return CustomResponse(await _authenticationService.GenerateJwt(userLoginModel.Email));
            else
                return CustomResponse("Login incorreto");
        }
    }
}
