using AutoMapper;
using Clients.Application.DTOs;
using Clients.Core.Entities;

namespace Clients.Application.Profiles
{
    /// <summary>
    /// ClientDto mapping configuration.
    /// </summary>
    class ClientsProfile : Profile
    {
        public ClientsProfile()
        {
            CreateMap<ClientDto, Client>()
                .ForMember(client => client.Id, action => action.Ignore());

            CreateMap<Client, ClientDto>();
        }
    }
}
