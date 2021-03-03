using Ardalis.GuardClauses;
using AutoMapper;
using Clients.Application.DTOs;
using Clients.Core.Contracts;
using Clients.Core.Entities;

namespace Clients.Application.Commands
{
    public class UpdateClientCommand : ICommand<ClientDto>
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

        public void Execute(ClientDto model)
        {
            Guard.Against.Null(model, nameof(model));

            var client = _repository.Get(x => x.Id == model.Id);

            Guard.Against.Null(client, nameof(client));

            _mapper.Map(model, client);

            var transaction = _repository.GetTransaction();

            transaction.Commit();
        }
    }
}
