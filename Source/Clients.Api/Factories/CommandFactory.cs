using System;
using Clients.Core.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Clients.Api.Factories
{
    /// <summary>
    /// Create an instance of an command.
    /// </summary>
    public class CommandFactory : ICommandFactory
    {
        protected readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="serviceProvider">DI container.</param>
        public CommandFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        public TCommand MakeCommand<TCommand>() where TCommand : ICommandBase
        {
            return _serviceProvider.GetRequiredService<TCommand>();
        }
    }
}
