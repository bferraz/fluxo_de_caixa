﻿using API.Caixa.Domain.Entities;
using API.Caixa.Domain.Repositories.Base;

namespace API.Caixa.Domain.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account GetAccount();

        void Update(Account account);
    }
}
