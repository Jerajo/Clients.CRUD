using System;

namespace Clients.Core.Contracts
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> MakePersistence<TEntity>() where TEntity : class, IEntity<Guid>;
    }
}
