using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using OrderManagmentAPI.Model;
using OrderManagmentAPI.Service.Dto;
using OrderManagmentAPI.Service.Interfaces;

namespace OrderManagmentAPI.Controllers
{
    [Route("Api/Order")]
    [ApiController]

    public class OrderController : ControllerBase
    {
        readonly IOrderService _orderService;
        readonly IClientService _clientService;
        IOrderItemService _OrderItemService;

        public OrderController(IOrderService orderService, IClientService clientService, IOrderItemService orderItemService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            _OrderItemService = orderItemService ?? throw new ArgumentNullException(nameof(orderItemService));

        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderDto>> GetOrders([FromQuery] OrderResourceParameter orderResourceParameters)
        {

            var AllOrders = _orderService.AllRows();
            return new JsonResult(AllOrders);

        }
        [HttpGet("{id}", Name = "GetOrderById")]
        public ActionResult GetOrderById(int Id)
        {
            var order = _orderService.FindById(Id);

            if (order == null)
            {
                return NotFound("This Order Id is not exist in database");
            }
            var orderItems = _OrderItemService.OrderItemsOfOrder(Id);
            var ret = new ReturnValues();
            ret.RetorderItems = orderItems;
            ret.RetOrder = order;
            return new JsonResult(ret);

        }


        [HttpPost]

        public ActionResult<OrderDto> PostOrder(OrderForCreationDto orderForCreationDto)
        {
            if (_clientService.FindById(orderForCreationDto.clientId) == null)
                return NotFound("This ClientId doesnt exist.");

            var OrderToReturn = _orderService.InsertOrder(orderForCreationDto);
            return CreatedAtRoute("GetOrderById", new { Id = OrderToReturn.id }, OrderToReturn);

        }
        [HttpPatch("{id}")]
        public ActionResult PatriallyUpdateOrder(int Id, JsonPatchDocument<OrderForUpdateDto> patchDocument)
        {
            //try
            // {
            _orderService.EditOrder(Id, patchDocument);
            return NoContent();
            //}
            // catch (NotFoundException)
            // {
            //   return NotFound("Jason parameters are not Correct or this Client Id is not exist in database.");
            // }
        }

    }
}
