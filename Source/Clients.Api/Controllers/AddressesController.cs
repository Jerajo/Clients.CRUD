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

            var addresses = getAddresses.Execute(x => true);

            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public IActionResult GetAddressById([FromRoute, FromQuery] Guid id)
        {
            var getAddressById = _queryFactory.MakeQuery<GetAddressByIdQuery>();

            var address = getAddressById.Execute(x => x.Id == id);

            if (address is null)
                return NotFound();

            return Ok(address);
        }

        [HttpPost]
        public IActionResult CreateAddress([FromBody] AddressDto addressDto)
        {
            var createAddress = _commandFactory.MakeCommand<CreateAddressCommand>();

            createAddress.Execute(addressDto);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAddressById([FromRoute, FromQuery] Guid id, [FromBody] AddressDto addressDto)
        {
            var updateAddress = _commandFactory.MakeCommand<UpdateAddressCommand>();

            updateAddress.Execute(addressDto);

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
