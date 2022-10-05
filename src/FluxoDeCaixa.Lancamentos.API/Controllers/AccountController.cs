using FluxoDeCaixa.Caixa.Domain.Entities;
using FluxoDeCaixa.Caixa.Domain.Repositories;
using FluxoDeCaixa.Caixa.Models;
using FluxoDeCaixa.Caixa.Services;
using API.Core.Controllers;
using API.Core.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FluxoDeCaixa.Caixa.Controllers
{
    [Authorize]
    [Route("api/account")]
    public class AccountController : BaseController
    {
        private readonly AccountService _accountService;
        private readonly IAccountRepository _accountRepository;
        private readonly IAspNetUser _user;
        private readonly IMapper _mapper;

        public AccountController(AccountService accountService,
            IAccountRepository accountRepository,
            IAspNetUser user, 
            IMapper mapper)
        {
            _accountService = accountService;
            _accountRepository = accountRepository;
            _user = user;
            _mapper = mapper;
        }

        [HttpGet("test")]
        [AllowAnonymous]
        public async Task<ActionResult> Test()
        {
            return CustomResponse();
        }

        [HttpGet("show-info")]
        public async Task<ActionResult> ShowAccountInfo()
        {
            try
            {
                var account = await _accountRepository.GetAccount();

                return CustomResponse(_mapper.Map<AccountModel>(account));
            }
            catch (Exception ex)
            {
                Erros.Add(ex.Message);

                return CustomResponse();
            }
        }

        [HttpPost("add-entry")]        
        public async Task<ActionResult> AddNewEntry(EntryModel entryModel)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                Guid userId = _user.ObterUserId();

                await _accountService.AddNewEntry(userId, _mapper.Map<Entry>(entryModel));

                return CustomResponse("Lançamento criado com sucesso");
            }
            catch (Exception ex)
            {
                Erros.Add(ex.Message);

                return CustomResponse();
            }
        }
    }
}
