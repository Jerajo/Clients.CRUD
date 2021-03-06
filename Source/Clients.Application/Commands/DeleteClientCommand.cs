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
            var transaction = _repository.GetTransaction();

            var client = _repository.Get(c => c.Id == clientId);

            client.DeleteFlag = "D";

            transaction.Commit();
        }
    }
}
