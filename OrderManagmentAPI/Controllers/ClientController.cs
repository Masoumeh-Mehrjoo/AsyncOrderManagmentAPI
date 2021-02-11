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
        public ActionResult PostClient(ClientForCreationDto Client)
        {
            var ClientToReturn = _clientService.InsertClient(Client);

            return CreatedAtRoute("GetClientBYId", new { Id = ClientToReturn.id }, ClientToReturn);

        }
        [HttpGet()]
        public ActionResult<ClientDto> GetClients([FromQuery] ClientResourceParameter ClientResourceParameter)
        {
            if (ClientResourceParameter == null && ClientResourceParameter.CRMId == 0)
            {
                var AllClients = _clientService.AllRows();
                return Ok(AllClients);
            }
            else
            {
                var AllClients = _clientService.SearchedRows(ClientResourceParameter);
                return Ok(AllClients);
            }
        }

        [HttpGet("{id}", Name = "GetClientBYId")]
        public ActionResult GetClientBYId(int Id)
        {
            var Client = _clientService.FindById(Id);
            if (Client == null)
            {
                return NotFound("This Client Id does not exist.");
            }

            return Ok(Client);
        }

        [HttpGet]
        [Route("OrdersOfClient/{ClientId}")]
        public ActionResult OrdersOfClient(int ClientId)
        {
            var orders = _clientService.OrdersofClient(ClientId);
            return Ok(orders);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteClient(int Id)
        {
            var Client = _clientService.FindById(Id);
            if (Client == null)
            {
                return NotFound("This Client Id does not exist.");

            }
            _clientService.DeleteClient(Id);
            return NoContent();

        }
        [HttpPatch("{id}")]
        public ActionResult PatriallyUpdateClient(int Id, JsonPatchDocument<ClientForUpdateDto> patchDocument)
        {
            try
            {
                _clientService.EditClient(Id, patchDocument);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound("Jason parameters are not Correct or this Client Id does not exist.");
            }
        }

    }
}
