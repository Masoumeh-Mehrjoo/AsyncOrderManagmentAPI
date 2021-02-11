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
        public ActionResult GetOrderItemBYId(int Id)
        {
            var orderItem = _OrderItemService.FindById(Id);
            if (orderItem == null)
            {
                return NotFound("This Client Id does not exist.");
            }

            return Ok(orderItem);
        }

        [HttpPost()]
        public ActionResult PostOrderItem(int OrderId, OrderItemForCreation orderItem)
        {
            if (_OrderService.FindById(OrderId) == null)
                return NotFound("This OrderId doesnt exist.");

            var PostedOrderItem = _OrderItemService.InsertOrderItem(OrderId, orderItem);
            return Ok(PostedOrderItem);

        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOrderItem(int OrderId, int Id)
        {
            if (_OrderService.FindById(OrderId) == null)
                return NotFound("This OrderId doesnt exist.");

            var orderItem = _OrderItemService.FindById(Id);

            if ((orderItem == null) || (OrderId != orderItem.OrderId))
                return NotFound("This OrderaItem Id or Order Id does not exist.");

            _OrderItemService.DeleteOrderItem(Id);
            return Ok("This OrderItem Deleted");
        }

        [HttpPatch("{id}")]
        public ActionResult PatriallyUpdateClient(int Id, JsonPatchDocument<OrderItemForUpdate> patchDocument)
        {
            try
            {
                _OrderItemService.EditOrderItem(Id, patchDocument);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound("Jason parameters are not Correct or this OrdrItem Id does not exist.");
            }
        }


    }
}
