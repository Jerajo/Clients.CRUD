using System;
using AutoMapper;
using Clients.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Clients.Api.Controllers
{
    /// <summary>
    /// Base controller class to get the dependency injections passed over other controllers. that inherits from this class.
    /// </summary>
    public class BaseController : ControllerBase
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICommandFactory _commandFactory;
        protected readonly IQueryFactory _queryFactory;
        protected readonly IRepositoryFactory _repositoryFactory;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        /// <summary>
        /// This is the default constructor. Is where the dependencies get injected.
        /// </summary>
        /// <param name="serviceProvider">The API current service provider or IoC Container.</param>
        public BaseController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _commandFactory = serviceProvider.GetRequiredService<ICommandFactory>();
            _queryFactory = serviceProvider.GetRequiredService<IQueryFactory>();
            _repositoryFactory = serviceProvider.GetRequiredService<IRepositoryFactory>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }
    }
}
