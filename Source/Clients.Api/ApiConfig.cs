using Clients.Api.Factories;
using Clients.Application.Commands;
using Clients.Application.Queries;
using Clients.Core.Contracts;
using Clients.SqlServer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Clients.Api
{
    public static class ApiConfig
    {
        public static void ConfigIoCForCommands(this IServiceCollection services)
        {
            services.AddScoped<CreateClientCommand>();
            services.AddScoped<DeleteClientCommand>();
            services.AddScoped<UpdateClientCommand>();

            services.AddScoped<CreateAddressCommand>();
            services.AddScoped<DeleteAddressCommand>();
            services.AddScoped<UpdateAddressCommand>();
        }

        public static void ConfigIoCForQueries(this IServiceCollection services)
        {
            services.AddScoped<GetClientsQuery>();
            services.AddScoped<GetClientByIdQuery>();

            services.AddScoped<GetAddressesQuery>();
            services.AddScoped<GetAddressByIdQuery>();
        }

        public static void ConfigIoCForFactories(this IServiceCollection services)
        {
            services.AddScoped<ICommandFactory, CommandFactory>();
            services.AddScoped<IQueryFactory, QueryFactory>();
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();
        }

        public static void ConfigIoCServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(DataRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
