using System;
using Ardalis.GuardClauses;
using AutoMapper;
using Clients.Application.DTOs;
using Clients.Core.Contracts;
using Clients.Core.Entities;

namespace Clients.Application.Queries
{
    public class GetClientByQuery : BaseQuery<Func<Client, bool>, ClientDto>
    {
        private readonly IRepository<Client> _repository;
        private readonly IMapper _mapper;

        public GetClientByQuery(IRepository<Client> repository, IMapper mapper)
        {
            Guard.Against.Null(repository, nameof(repository));
            Guard.Against.Null(mapper, nameof(mapper));

            _repository = repository;
            _mapper = mapper;
        }

        public override ClientDto Execute(Func<Client, bool> query)
        {
            Guard.Against.Null(query, nameof(query));

            var client = _repository.Get(query);

            var clientDto = _mapper.Map<ClientDto>(client);

            return clientDto;
        }
    }
}
