using System;

namespace Clients.Core.Contracts
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> MakeRepository<TEntity>() where TEntity : class, IEntity<Guid>;
    }
}
