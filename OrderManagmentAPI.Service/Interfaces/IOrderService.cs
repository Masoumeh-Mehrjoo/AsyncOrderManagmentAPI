using Microsoft.AspNetCore.JsonPatch;
using OrderManagmentAPI.Model;
using OrderManagmentAPI.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentAPI.Service.Interfaces
{
     public interface IOrderService
    {
        public Task <OrderDto> InsertOrderAsync(OrderForCreationDto Order);

        public Task<IEnumerable<OrderDto>> AllRowsAsync();

        public Task<OrderDto> FindByIdAsync(int Id);

        public Task<IEnumerable<OrderDto>> SearchedRowsAsync(OrderResourceParameter OrderResourceParameter);

        public Task DeleteOrderAsync(int Id);

        public Task EditOrderAsync(int OrderId, JsonPatchDocument<OrderForUpdateDto> patchDocument);
    }
}
