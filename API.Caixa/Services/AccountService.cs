using API.Caixa.Domain.Entities;
using API.Caixa.Domain.Exceptions;
using API.Caixa.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace API.Caixa.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<bool> AddNewEntry(Guid UserId, Entry entry)
        {
            try
            {
                Account account = await _accountRepository.GetAccount();

                entry.UserId = UserId;
                entry.AccountId = account.Id;

                account.UpdateValue(entry);

                _accountRepository.Update(account);
                _accountRepository.AddEntry(entry);

                return await _accountRepository.UnitOfWork.Commit();
            }
            catch (InvalidCreditException)
            {
                throw new Exception("Valor de crédito inválido");
            }
            catch (InvalidDebitException)
            {
                throw new Exception("Valor de débito inválido");
            }
            catch (InsuficientFoundsException)
            {
                throw new Exception("Saldo insuficiente para realizar esta operação");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
