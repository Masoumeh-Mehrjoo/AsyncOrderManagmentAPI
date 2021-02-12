using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using OrderManagmentAPI.Model;
using OrderManagmentAPI.Repository;
using OrderManagmentAPI.Service.Dto;
using OrderManagmentAPI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagmentAPI.Controllers
{
    [Route("api/Client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
        }

        [HttpPost]
        public async Task<ActionResult> PostClient(ClientForCreationDto Client)
        {
            var ClientToReturn = await _clientService.InsertClientAsync(Client);

            return CreatedAtRoute("GetClientBYId", new { Id = ClientToReturn.id }, ClientToReturn);

        }
        [HttpGet()]
        public async Task<ActionResult<ClientDto>> GetClients([FromQuery] ClientResourceParameter ClientResourceParameter)
        {
            if (ClientResourceParameter == null && ClientResourceParameter.CRMId == 0)
            {
                var AllClients = await _clientService.AllRowsAsync();
                return Ok(AllClients);
            }
            else
            {
                var AllClients = await _clientService.SearchedRowsAsync(ClientResourceParameter);
                return Ok(AllClients);

            }
        }

        [HttpGet("{id}", Name = "GetClientBYId")]
        public async Task<ActionResult> GetClientBYId(int Id)
        {
            var Client = await _clientService.FindByIdAsync(Id);
            if (Client == null)
            {
                return NotFound("This Client Id does not exist.");
            }

            return Ok(Client);
        }

        [HttpGet]
        [Route("OrdersOfClient/{ClientId}")]
        public async Task<ActionResult> OrdersOfClient(int ClientId)
        {
            var orders = await _clientService.OrdersofClientAsync(ClientId);
            return Ok(orders);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int Id)
        {
            var Client = await _clientService.FindByIdAsync(Id);
            if (Client == null)
            {
                return NotFound("This Client Id does not exist.");

            }
            await _clientService.DeleteClientAsync(Id);
            return NoContent();

        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatriallyUpdateClient(int Id, JsonPatchDocument<ClientForUpdateDto> patchDocument)
        {
            try
            {
                await _clientService.EditClientAsync(Id, patchDocument);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound("Jason parameters are not Correct or this Client Id does not exist.");
            }
        }

    }
}
