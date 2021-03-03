using Ardalis.GuardClauses;
using AutoMapper;
using Clients.Application.DTOs;
using Clients.Core.Contracts;
using Clients.Core.Entities;

namespace Clients.Application.Commands
{
    public class UpdateAddressCommand : ICommand<AddressDto>
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

        public void Execute(AddressDto model)
        {
            Guard.Against.Null(model, nameof(model));

            var address = _repository.Get(x => x.Id == model.Id);

            Guard.Against.Null(address, nameof(address));

            _mapper.Map(model, address);

            var transaction = _repository.GetTransaction();

            transaction.Commit();
        }
    }
}
