using Microsoft.AspNetCore.JsonPatch;
using OrderManagmentAPI.Model;
using OrderManagmentAPI.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentAPI.Service.Interfaces
{
     public interface IOrderService
    {
        public OrderDto InsertOrder(OrderForCreationDto Order);

        public IEnumerable<OrderDto> AllRows();

        public OrderDto FindById(int Id);

        public IEnumerable<OrderDto> SearchedRows(OrderResourceParameter OrderResourceParameter);

        public void DeleteOrder(int Id);

        public void EditOrder(int OrderId, JsonPatchDocument<OrderForUpdateDto> patchDocument);
    }
}
