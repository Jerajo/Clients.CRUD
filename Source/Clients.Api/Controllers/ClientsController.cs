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
    public class ClientsController : BaseController
    {
        public ClientsController(IServiceProvider serviceProvider)
            : base(serviceProvider) { }


        [HttpGet]
        public IActionResult GetClients()
        {
            var getClients = _queryFactory.MakeQuery<GetClientsQuery>();

            var clients = getClients.Execute(x => true);

            return Ok(clients);
        }

        [HttpGet]
        public IActionResult GetClientById([FromRoute, FromQuery] Guid clientId)
        {
            if (clientId == Guid.Empty)
                return BadRequest();

            var getClientById = _queryFactory.MakeQuery<GetClientByIdQuery>();

            var client = getClientById.Execute(x => x.Id == clientId);

            if (client is null)
                return NotFound();

            return Ok(client);
        }

        [HttpPost]
        public IActionResult CreateClient([FromBody] ClientDto clientDto)
        {
            Guard.Against.Null(clientDto, nameof(clientDto));

            var createClient = _commandFactory.MakeCommand<CreateClientCommand>();

            createClient.Execute(clientDto);

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateClientById([FromRoute, FromQuery] Guid clientId, [FromBody] ClientDto clientDto)
        {
            if (clientId == Guid.Empty || clientId != clientDto.Id)
                return BadRequest();
            Guard.Against.Null(clientDto, nameof(clientDto));

            var updateClient = _commandFactory.MakeCommand<UpdateClientCommand>();
            updateClient.Execute(clientDto);

            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteClientById([FromRoute, FromQuery] Guid clientId)
        {
            if (clientId == Guid.Empty)
                return BadRequest();

            var deleteClient = _commandFactory.MakeCommand<DeleteClientCommand>();
            deleteClient.Execute(clientId);

            return Ok();
        }
    }
}