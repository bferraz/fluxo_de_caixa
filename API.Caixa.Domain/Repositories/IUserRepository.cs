using API.Caixa.Domain.Entities;
using API.Caixa.Domain.Repositories.Base;
using System;
using System.Threading.Tasks;

namespace API.Caixa.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByCpf(string cpf);

        Task<User> GetUserById(Guid id);

        void Insert(User user);
    }
}
