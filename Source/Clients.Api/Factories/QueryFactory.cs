using System;
using Clients.Core.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Clients.Api.Factories
{
    /// <summary>
    /// Create an instance of an query.
    /// </summary>
    public class QueryFactory : IQueryFactory
    {
        protected readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="serviceProvider">DI container.</param>
        public QueryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        public TQuery MakeQuery<TQuery>() where TQuery : IQueryBase
        {
            return _serviceProvider.GetRequiredService<TQuery>();
        }
    }
}
