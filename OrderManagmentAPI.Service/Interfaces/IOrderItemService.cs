using Microsoft.AspNetCore.JsonPatch;
using OrderManagmentAPI.Model;
using OrderManagmentAPI.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentAPI.Service.Interfaces
{
    public interface IOrderItemService
    {
        public OrderItemDto InsertOrderItem(int orderId, OrderItemForCreation OrderItem);
        public OrderItemDto FindById(int Id);
        public void DeleteOrderItem(int Id);
        public void EditOrderItem(int Id, JsonPatchDocument<OrderItemForUpdate> patchDocument);
        public IEnumerable<OrderItemDto> OrderItemsOfOrder(int OrderId);
    }
}
