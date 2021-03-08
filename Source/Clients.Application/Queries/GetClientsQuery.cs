using System;
using System.Collections.Generic;
using System.Linq;
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

            var clients = _repository.Query(query,
                c => new Client {
                    Id = c.Id,
                    FullName = c.FullName,
                    UserName = c.UserName,
                    Email = c.Email,
                    BirthDay = c.BirthDay,
                    MarriageStatus = c.MarriageStatus,
                    Addresses = c.Addresses.Where(a => string.IsNullOrEmpty(a.DeleteFlag))
                });

            var clientsDto = _mapper.Map<List<ClientDto>>(clients);

            return clientsDto;
        }
    }
}
