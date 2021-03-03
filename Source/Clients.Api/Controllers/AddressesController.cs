using System;
using Ardalis.GuardClauses;
using Clients.Application.Commands;
using Clients.Application.DTOs;
using Clients.Application.Queries;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{addressId}")]
        public IActionResult GetAddressById([FromRoute, FromQuery] Guid addressId)
        {
            if (addressId == Guid.Empty)
                return BadRequest();

            var getAddressById = _queryFactory.MakeQuery<GetAddressByIdQuery>();

            var address = getAddressById.Execute(x => x.Id == addressId);

            if (address is null)
                return NotFound();

            return Ok(address);
        }

        [HttpPost]
        public IActionResult CreateAddress([FromBody] AddressDto addressDto)
        {
            Guard.Against.Null(addressDto, nameof(addressDto));

            var createAddress = _commandFactory.MakeCommand<CreateAddressCommand>();
            try
            {
                createAddress.Execute(addressDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPut("{addressId}")]
        public IActionResult UpdateAddressById([FromRoute, FromQuery] Guid addressId, [FromBody] AddressDto addressDto)
        {
            if (addressId == Guid.Empty || addressId != addressDto.Id)
                return BadRequest();
            Guard.Against.Null(addressDto, nameof(addressDto));

            var updateAddress = _commandFactory.MakeCommand<UpdateAddressCommand>();
            try
            {
                updateAddress.Execute(addressDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpDelete("{addressId}")]
        public IActionResult DeleteAddressById([FromRoute, FromQuery] Guid addressId)
        {
            if (addressId == Guid.Empty)
                return BadRequest();

            var deleteAddress = _commandFactory.MakeCommand<DeleteAddressCommand>();
            try
            {
                deleteAddress.Execute(addressId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}
