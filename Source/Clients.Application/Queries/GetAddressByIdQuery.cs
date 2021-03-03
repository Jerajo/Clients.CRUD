using System;
using Ardalis.GuardClauses;
using AutoMapper;
using Clients.Application.DTOs;
using Clients.Core.Contracts;
using Clients.Core.Entities;

namespace Clients.Application.Queries
{
    public class GetAddressByIdQuery : BaseQuery<Func<Address, bool>, AddressDto>
    {
        private readonly IRepository<Address> _repository;
        private readonly IMapper _mapper;

        public GetAddressByIdQuery(IRepository<Address> repository, IMapper mapper)
        {
            Guard.Against.Null(repository, nameof(repository));
            Guard.Against.Null(mapper, nameof(mapper));

            _repository = repository;
            _mapper = mapper;
        }

        public override AddressDto Execute(Func<Address, bool> query)
        {
            Guard.Against.Null(query, nameof(query));

            var address = _repository.Get(query);

            var addressDto = _mapper.Map<AddressDto>(address);

            return addressDto;
        }
    }
}
