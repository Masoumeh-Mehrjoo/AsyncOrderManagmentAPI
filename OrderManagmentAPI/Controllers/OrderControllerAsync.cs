using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using OrderManagmentAPI.Model;
using OrderManagmentAPI.Repository;
using OrderManagmentAPI.Service.Dto;
using OrderManagmentAPI.Service.Interfaces;

namespace OrderManagmentAPI.Controllers
{
    [Route("Api/Order")]
    [ApiController]

    public class OrderControllerAsync : ControllerBase
    {
        readonly IOrderService _orderService;
        readonly IClientService _clientService;
        IOrderItemService _OrderItemService;

        public OrderControllerAsync(IOrderService orderService, IClientService clientService, IOrderItemService orderItemService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            _OrderItemService = orderItemService ?? throw new ArgumentNullException(nameof(orderItemService));

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersAsync([FromQuery] OrderResourceParameter orderResourceParameters)
        {

            var AllOrders = await _orderService.AllRowsAsync();
            return new JsonResult(AllOrders);

        }
        [HttpGet("{id}", Name = "GetOrderById")]
        public async Task<ActionResult> GetOrderByIdAsync(int Id)
        {
            var order = await _orderService.FindByIdAsync(Id);

            if (order == null)
            {
                return NotFound("This Order Id is not exist in database");
            }
            var orderItems = await _OrderItemService.OrderItemsOfOrderAsync(Id);
            var ret = new ReturnValues();
            ret.RetorderItems = orderItems;
            ret.RetOrder = order;
            return new JsonResult(ret);

        }


        [HttpPost]

        public async Task<ActionResult<OrderDto>> PostOrdeAsyncr(OrderForCreationDto orderForCreationDto)
        {
            if ((await _clientService.FindByIdAsync(orderForCreationDto.clientId)) == null)
                return NotFound("This ClientId doesnt exist.");

            var OrderToReturn = await _orderService.InsertOrderAsync(orderForCreationDto);
            return CreatedAtRoute("GetOrderById", new { Id = OrderToReturn.id }, OrderToReturn);

        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatriallyUpdateOrderAsync(int Id, JsonPatchDocument<OrderForUpdateDto> patchDocument)
        {
            try
            {
                await _orderService.EditOrderAsync(Id, patchDocument);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound("Jason parameters are not Correct or this Client Id is not exist in database.");
            }
        }

    }
}
