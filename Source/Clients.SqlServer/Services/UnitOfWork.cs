using System;
using System.Threading.Tasks;
using Clients.Core.Contracts;

namespace Clients.SqlServer.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _dBContext;

        public UnitOfWork(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        #region Interface Methods

        public void Commit()
        {
            _dBContext.SaveChanges();
        }

        public Task CommitAsync()
        {
            return _dBContext.SaveChangesAsync();
        }

        public IRepository<T> GetRepository<T>() where T : class, IEntity<Guid>
        {
            return new DataRepository<T>(_dBContext);
        }

        #endregion
    }
}
