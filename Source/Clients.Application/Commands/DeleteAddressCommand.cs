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
            if (addressId == Guid.Empty)
                throw new Exception($"A valid client id was expected, but got: {addressId}");

            var transaction = _repository.GetTransaction();

            _repository.Delete(new Address { Id = addressId });

            transaction.Commit();
        }
    }
}
