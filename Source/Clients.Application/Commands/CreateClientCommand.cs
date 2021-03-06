﻿using Ardalis.GuardClauses;
using AutoMapper;
using Clients.Application.DTOs;
using Clients.Core.Contracts;
using Clients.Core.Entities;

namespace Clients.Application.Commands
{
    public class CreateClientCommand : ICommand<ClientForCreationDto>
    {
        private readonly IRepository<Client> _repository;
        private readonly IMapper _mapper;

        public CreateClientCommand(IRepository<Client> repository, IMapper mapper)
        {
            Guard.Against.Null(repository, nameof(repository));
            Guard.Against.Null(mapper, nameof(mapper));

            _repository = repository;
            _mapper = mapper;
        }

        public void Execute(ClientForCreationDto model)
        {
            Guard.Against.Null(model, nameof(model));

            var newClient = _mapper.Map<Client>(model);

            var transaction = _repository.GetTransaction();

            _repository.Add(newClient);

            transaction.Commit();
        }
    }
}
