using System.Threading.Tasks;

namespace API.Caixa.Domain.Repositories.Base
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
