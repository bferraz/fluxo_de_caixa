using System;

namespace API.Caixa.Domain.Repositories.Base
{
    public interface IRepository<T> : IDisposable where T : IAggregationRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
