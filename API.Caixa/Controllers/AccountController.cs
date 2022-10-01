﻿using API.Caixa.Domain.Entities;
using API.Caixa.Models;
using API.Caixa.Services;
using API.Core.Controllers;
using API.Core.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Caixa.Controllers
{
    [Authorize]
    [Route("api/account")]
    public class AccountController : BaseController
    {
        private readonly AccountService _accountService;
        private readonly IAspNetUser _user;
        private readonly IMapper _mapper;

        public AccountController(AccountService 
            accountService, IAspNetUser user,
            IMapper mapper)
        {
            _accountService = accountService;
            _user = user;
            _mapper = mapper;
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
