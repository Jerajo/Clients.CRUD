using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using AutoMapper;
using Clients.Application.DTOs;
using Clients.Core.Contracts;
using Clients.Core.Entities;

namespace Clients.Application.Queries
{
    public class GetAddressesQuery : BaseQuery<Func<Address, bool>, List<AddressDto>>
    {
        private readonly IRepository<Address> _repository;
        private readonly IMapper _mapper;

        public GetAddressesQuery(IRepository<Address> repository, IMapper mapper)
        {
            Guard.Against.Null(repository, nameof(repository));
            Guard.Against.Null(mapper, nameof(mapper));

            _repository = repository;
            _mapper = mapper;
        }

        public override List<AddressDto> Execute(Func<Address, bool> query)
        {
            Guard.Against.Null(query, nameof(query));

            var addresses = _repository.Query(query);

            var addressesDto = _mapper.Map<List<AddressDto>>(addresses);

            return addressesDto;
        }
    }
}