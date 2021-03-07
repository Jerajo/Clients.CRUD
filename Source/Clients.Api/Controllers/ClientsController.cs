using System;
using Microsoft.AspNetCore.Mvc;

using Clients.Application.Commands;
using Clients.Application.DTOs;
using Clients.Application.Queries;

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

            var clients = getClients.Execute(c =>
                string.IsNullOrEmpty(c.DeleteFlag));

            return Ok(clients);
        }

        [HttpGet("{id}")]
        public IActionResult GetClientById([FromRoute, FromQuery] Guid id)
        {
            var getClientById = _queryFactory.MakeQuery<GetClientByIdQuery>();

            var client = getClientById.Execute(c =>
                    c.Id == id &&
                    string.IsNullOrEmpty(c.DeleteFlag));

            if (client is null)
                return NotFound();

            return Ok(client);
        }

        [HttpPost]
        public IActionResult CreateClient([FromBody] ClientForCreationDto clientDto)
        {
            var createClient = _commandFactory.MakeCommand<CreateClientCommand>();

            createClient.Execute(clientDto);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClientById(
            [FromRoute, FromQuery] Guid id,
            [FromBody] ClientForEditionDto clientDto)
        {
            var updateClient = _commandFactory.MakeCommand<UpdateClientCommand>();

            updateClient.Execute((id, clientDto));

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClientById([FromRoute, FromQuery] Guid id)
        {
            var deleteClient = _commandFactory.MakeCommand<DeleteClientCommand>();

            deleteClient.Execute(id);

            return Ok();
        }
    }
}
