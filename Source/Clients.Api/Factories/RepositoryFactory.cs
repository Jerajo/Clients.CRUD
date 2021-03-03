using System;
using Clients.Core.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Clients.Api.Factories
{
    /// <summary>
    /// Create an instance of an command.
    /// </summary>
    public class RepositoryFactory : IRepositoryFactory
    {
        protected readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="serviceProvider">DI container.</param>
        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        public IRepository<TEntity> MakeRepository<TEntity>() where TEntity : class, IEntity<Guid>
        {
            return _serviceProvider.GetRequiredService<IRepository<TEntity>>();
        }
    }
}
