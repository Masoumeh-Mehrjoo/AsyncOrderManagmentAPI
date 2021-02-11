using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentAPI.Model.Repository
{
    public interface IOrderRepository : IRepository<Order, int, OrderResourceParameter>
    {
        public void AddNewOrderItem(Order order,OrderItem orderItem);
        public void DeleteOrderItem(Order entity,OrderItem orderItem);
        public void EditOrderItem(Order entity, OrderItem OldOrderItem, OrderItem NewOrderItem);
            
    }
}
