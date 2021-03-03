using System;
using Ardalis.GuardClauses;
using AutoMapper;
using Clients.Core.Contracts;
using Clients.Core.Entities;

namespace Clients.Application.Commands
{
    public class DeleteClientCommand : ICommand<Guid>
    {
        private readonly IRepository<Client> _repository;
        private readonly IMapper _mapper;

        public DeleteClientCommand(IRepository<Client> repository, IMapper mapper)
        {
            Guard.Against.Null(repository, nameof(repository));
            Guard.Against.Null(mapper, nameof(mapper));

            _repository = repository;
            _mapper = mapper;
        }

        public void Execute(Guid clientId)
        {
            if (clientId == Guid.Empty)
                throw new Exception($"A valid client id was expected, but got: {clientId}");

            var transaction = _repository.GetTransaction();

            _repository.Delete(new Client { Id = clientId });

            transaction.Commit();
        }
    }
}
