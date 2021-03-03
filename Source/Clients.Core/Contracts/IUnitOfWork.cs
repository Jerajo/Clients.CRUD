using System;
using System.Threading.Tasks;

namespace Clients.Core.Contracts
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetPersistence<TEntity>(Type typeEntity)
             where TEntity : class, IEntity<Guid>;
        void CommitChanges(Action<string> errorCallback = null);
        Task CommitChangesAsync(Action<string> errorCallback = null);
    }
}
