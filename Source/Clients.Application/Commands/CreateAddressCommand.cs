using Ardalis.GuardClauses;
using AutoMapper;
using Clients.Application.DTOs;
using Clients.Core.Contracts;
using Clients.Core.Entities;

namespace Clients.Application.Commands
{
    public class CreateAddressCommand : ICommand<AddressDto>
    {
        private readonly IRepository<Address> _repository;
        private readonly IMapper _mapper;

        public CreateAddressCommand(IRepository<Address> repository, IMapper mapper)
        {
            Guard.Against.Null(repository, nameof(repository));
            Guard.Against.Null(mapper, nameof(mapper));

            _repository = repository;
            _mapper = mapper;
        }

        public void Execute(AddressDto model)
        {
            Guard.Against.Null(model, nameof(model));

            var newAddress = _mapper.Map<Address>(model);

            var transaction = _repository.GetTransaction();

            _repository.Add(newAddress);

            transaction.Commit();
        }
    }
}
