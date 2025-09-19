using ClientManagement.Application.Dto;
using ClientManagement.Application.Interfaces.Services;
using ClientManagement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDTO>> GetById(int id)
        {
            var client = await _clientService.GetById(id);
            if (client == null)
                return NotFound();
            return Ok(client);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<ClientDTO>>> GetAll()
        {
            var clients = await _clientService.GetAll();
            return Ok(clients);
        }

        [HttpPost]
        public async Task<ActionResult<ClientDTO>> Add([FromBody] ClientDTO client)
        {
            if (client == null)
                return BadRequest();

            var createdClient = await _clientService.Add(client);
            return CreatedAtAction(nameof(GetById), new { id = createdClient.Id }, createdClient);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Client>> Update(int id, [FromBody] ClientDTO client)
        {
            if (client == null || client.Id != id)
                return BadRequest();

            var updatedClient = await _clientService.Update(client);
            if (updatedClient == null)
                return NotFound();

            return Ok(updatedClient);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _clientService.Delete(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
