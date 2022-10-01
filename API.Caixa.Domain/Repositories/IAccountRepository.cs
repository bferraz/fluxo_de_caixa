using API.Caixa.Domain.Entities;
using API.Caixa.Domain.Repositories.Base;
using System.Threading.Tasks;

namespace API.Caixa.Domain.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetAccount();

        void Update(Account account);

        void AddEntry(Entry entry);
    }
}
