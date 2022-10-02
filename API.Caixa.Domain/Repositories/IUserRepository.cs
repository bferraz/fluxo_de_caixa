using API.Caixa.Domain.Entities;
using API.Caixa.Domain.Repositories.Base;
using System.Threading.Tasks;

namespace API.Caixa.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByCpf(string cpf);

        void Insert(User user);
    }
}
