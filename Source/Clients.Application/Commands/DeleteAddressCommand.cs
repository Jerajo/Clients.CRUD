using System;
using Ardalis.GuardClauses;
using AutoMapper;
using Clients.Core.Contracts;
using Clients.Core.Entities;

namespace Clients.Application.Commands
{
    public class DeleteAddressCommand : ICommand<Guid>
    {
        private readonly IRepository<Address> _repository;
        private readonly IMapper _mapper;

        public DeleteAddressCommand(IRepository<Address> repository, IMapper mapper)
        {
            Guard.Against.Null(repository, nameof(repository));
            Guard.Against.Null(mapper, nameof(mapper));

            _repository = repository;
            _mapper = mapper;
        }

        public void Execute(Guid addressId)
        {
            var transaction = _repository.GetTransaction();

            var address = _repository.Get(c => c.Id == addressId);

            address.DeleteFlag = "D";

            transaction.Commit();
        }
    }
}
