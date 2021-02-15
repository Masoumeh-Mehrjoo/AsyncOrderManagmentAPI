using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentAPI.Model.Repository
{
    public interface IOrderRepository : IRepositortyC <Order, int, OrderResourceParameter>
    {
        public Task AddNewOrderItemAsync(Order order,OrderItem orderItem);
        public Task DeleteOrderItemAsync(Order entity,OrderItem orderItem);
        public Task EditOrderItemAsync(Order entity, OrderItem OldOrderItem, OrderItem NewOrderItem);
            
    }
}
