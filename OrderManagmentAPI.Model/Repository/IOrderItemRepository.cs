using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentAPI.Model.Repository
{
    public interface IOrderItemRepository : IRepository<OrderItem, int, OrderItemResourceParameter>
    {
        public OrderItem InsertByOrderId(int OrderId, OrderItem orderItem);
        public IEnumerable<OrderItem> FindOrderItemsofOrderId(int OrderId);
    }
}
