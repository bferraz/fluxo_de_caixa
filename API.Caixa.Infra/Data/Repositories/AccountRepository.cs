using API.Caixa.Domain.Entities;
using API.Caixa.Domain.Repositories;
using API.Caixa.Domain.Repositories.Base;
using System.Threading.Tasks;

namespace API.Caixa.Infra.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly CashierContext _context;

        public AccountRepository(CashierContext cashierContext)
        {
            _context = cashierContext;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Account> GetAccount()
        {
            return await _context.Accounts.FindAsync();
        }

        public void Update(Account account)
        {
            _context.Accounts.Update(account);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }        
    }
}
