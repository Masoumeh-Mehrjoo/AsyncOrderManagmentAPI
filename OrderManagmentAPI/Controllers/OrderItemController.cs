using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using OrderManagmentAPI.Repository;
using OrderManagmentAPI.Service;
using OrderManagmentAPI.Service.Dto;
using OrderManagmentAPI.Service.Interfaces;

namespace OrderManagmentAPI.Controllers
{
    [Route("api/Order/{OrderId}/OrderItems")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        IOrderItemService _OrderItemService;
        IOrderService _OrderService;
        public OrderItemController(IOrderItemService orderItemService, IOrderService orderService)
        {
            _OrderItemService = orderItemService ?? throw new ArgumentNullException(nameof(orderItemService));
            _OrderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }
        [HttpGet("{id}", Name = "GetOrderItemBYId")]
        public async Task<ActionResult> GetOrderItemBYId(int Id)
        {
            var orderItem =await _OrderItemService.FindByIdAsync(Id);
            if (orderItem == null)
            {
                return NotFound("This Client Id does not exist.");
            }

            return Ok(orderItem);
        }

        [HttpPost()]
        public async Task<ActionResult> PostOrderItem(int OrderId, OrderItemForCreation orderItem)
        {
            if (_OrderService.FindByIdAsync(OrderId) == null)
                return NotFound("This OrderId doesnt exist.");

            var PostedOrderItem =await _OrderItemService.InsertOrderItemAsync(OrderId, orderItem);
            return Ok(PostedOrderItem);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderItem(int OrderId, int Id)
        {
            if (_OrderService.FindByIdAsync(OrderId) == null)
                return NotFound("This OrderId doesnt exist.");

            var orderItem =await _OrderItemService.FindByIdAsync(Id);

            if ((orderItem == null) || (OrderId != orderItem.OrderId))
                return NotFound("This OrderaItem Id or Order Id does not exist.");

            await _OrderItemService.DeleteOrderItemAsync(Id);
            return Ok("This OrderItem Deleted");
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatriallyUpdateClient(int OrderId,int Id, JsonPatchDocument<OrderItemForUpdate> patchDocument)
        {
            try
            {
                if (_OrderService.FindByIdAsync(OrderId) == null )
                    return NotFound("This OrderId doesnt exist.");

                await  _OrderItemService.EditOrderItemAsync(Id, patchDocument);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound("Jason parameters are not Correct or this OrdrItem Id does not exist.");
            }
        }


    }
}
