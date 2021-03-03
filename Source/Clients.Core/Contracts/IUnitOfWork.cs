using System;
using System.Threading.Tasks;

namespace Clients.Core.Contracts
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class, IEntity<Guid>;
        void Commit();
        Task CommitAsync();
    }
}
