using System;
using Ardalis.GuardClauses;
using AutoMapper;
using Clients.Application.DTOs;
using Clients.Core.Contracts;
using Clients.Core.Entities;

namespace Clients.Application.Commands
{
    public class UpdateClientCommand : ICommand<(Guid, ClientForEditionDto)>
    {
        private readonly IRepository<Client> _repository;
        private readonly IMapper _mapper;

        public UpdateClientCommand(IRepository<Client> repository, IMapper mapper)
        {
            Guard.Against.Null(repository, nameof(repository));
            Guard.Against.Null(mapper, nameof(mapper));

            _repository = repository;
            _mapper = mapper;
        }

        public void Execute((Guid, ClientForEditionDto) modelData)
        {
            Guard.Against.Null(modelData, nameof(modelData));

            (Guid clientId, ClientForEditionDto model) = modelData;

            var client = _repository.Get(x => x.Id == clientId);

            _mapper.Map(model, client);

            var transaction = _repository.GetTransaction();

            transaction.Commit();
        }
    }
}
