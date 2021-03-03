using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using AutoMapper;
using Clients.Application.DTOs;
using Clients.Core.Contracts;
using Clients.Core.Entities;

namespace Clients.Application.Queries
{
    public class GetClientsQuery : BaseQuery<Func<Client, bool>, List<ClientDto>>
    {
        private readonly IRepository<Client> _repository;
        private readonly IMapper _mapper;

        public GetClientsQuery(IRepository<Client> repository, IMapper mapper)
        {
            Guard.Against.Null(repository, nameof(repository));
            Guard.Against.Null(mapper, nameof(mapper));

            _repository = repository;
            _mapper = mapper;
        }

        public override List<ClientDto> Execute(Func<Client, bool> query)
        {
            Guard.Against.Null(query, nameof(query));

            var clients = _repository.Query(query);

            var clientsDto = _mapper.Map<List<ClientDto>>(clients);

            return clientsDto;
        }
    }
}
