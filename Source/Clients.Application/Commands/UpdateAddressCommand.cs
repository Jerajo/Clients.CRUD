using System;

using Ardalis.GuardClauses;
using AutoMapper;

using Clients.Application.DTOs;
using Clients.Core.Contracts;
using Clients.Core.Entities;

namespace Clients.Application.Commands
{
    public class UpdateAddressCommand : ICommand<(Guid, AddressForEditionDto)>
    {
        private readonly IRepository<Address> _repository;
        private readonly IMapper _mapper;

        public UpdateAddressCommand(IRepository<Address> repository, IMapper mapper)
        {
            Guard.Against.Null(repository, nameof(repository));
            Guard.Against.Null(mapper, nameof(mapper));

            _repository = repository;
            _mapper = mapper;
        }

        public void Execute((Guid, AddressForEditionDto) modelData)
        {
            Guard.Against.Null(modelData, nameof(modelData));

            (Guid addressId, AddressForEditionDto model) = modelData;

            var address = _repository.Get(x => x.Id == addressId);

            _mapper.Map(model, address);

            var transaction = _repository.GetTransaction();

            transaction.Commit();
        }
    }
}
