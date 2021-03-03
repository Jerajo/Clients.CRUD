using AutoMapper;
using Clients.Application.DTOs;
using Clients.Core.Entities;

namespace Clients.Application.Profiles
{
    public class AddressesProfile : Profile
    {
        public AddressesProfile()
        {
            CreateMap<AddressDto, Address>()
                .ForMember(address => address.Id, action => action.Ignore());

            CreateMap<Address, AddressDto>();
        }
    }
}
