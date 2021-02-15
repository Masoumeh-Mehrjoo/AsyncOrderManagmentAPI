using Microsoft.AspNetCore.JsonPatch;
using OrderManagmentAPI.Model;
using OrderManagmentAPI.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentAPI.Service.Interfaces
{
    public interface IOrderItemService
    {
        public Task<OrderItemDto> InsertOrderItemAsync(int orderId, OrderItemForCreation OrderItem);
        public Task<OrderItemDto> FindByIdAsync(int Id);
        public Task DeleteOrderItemAsync(int Id);
        public Task EditOrderItemAsync(int Id, JsonPatchDocument<OrderItemForUpdate> patchDocument);
        public Task<IEnumerable<OrderItemDto>> OrderItemsOfOrderAsync(int OrderId);
    }
}
