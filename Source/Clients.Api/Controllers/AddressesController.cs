using System;
using Microsoft.AspNetCore.Mvc;

using Clients.Application.Commands;
using Clients.Application.DTOs;
using Clients.Application.Queries;

namespace Clients.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressesController : BaseController
    {
        public AddressesController(IServiceProvider serviceProvider)
            : base(serviceProvider) { }

        [HttpGet]
        public IActionResult GetAddresses()
        {
            var getAddresses = _queryFactory.MakeQuery<GetAddressesQuery>();

            var addresses = getAddresses.Execute(a =>
                string.IsNullOrEmpty(a.DeleteFlag));

            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public IActionResult GetAddressById([FromRoute, FromQuery] Guid id)
        {
            var getAddressById = _queryFactory.MakeQuery<GetAddressByQuery>();

            var address = getAddressById.Execute(a =>
                a.Id == id &&
                string.IsNullOrEmpty(a.DeleteFlag));

            if (address is null)
                return NotFound();

            return Ok(address);
        }

        [HttpPost]
        public IActionResult CreateAddress([FromBody] AddressForCreationDto addressDto)
        {
            var createAddress = _commandFactory.MakeCommand<CreateAddressCommand>();

            createAddress.Execute(addressDto);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAddressById([FromRoute, FromQuery] Guid id, [FromBody] AddressForEditionDto addressDto)
        {
            var updateAddress = _commandFactory.MakeCommand<UpdateAddressCommand>();

            updateAddress.Execute((id, addressDto));

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAddressById([FromRoute, FromQuery] Guid id)
        {
            var deleteAddress = _commandFactory.MakeCommand<DeleteAddressCommand>();

            deleteAddress.Execute(id);

            return Ok();
        }
    }
}
