using API.Caixa.Domain.Entities;
using API.Caixa.Domain.Repositories;
using API.Caixa.Domain.Repositories.Base;

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

        //public void Add(Entry entry)
        //{
        //    _context.Entries.Add(entry);
        //}

        public void Update(Account account)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }        
    }
}
