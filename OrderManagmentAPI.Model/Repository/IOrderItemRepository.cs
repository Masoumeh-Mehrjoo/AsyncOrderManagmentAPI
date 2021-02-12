using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentAPI.Model.Repository
{
    public interface IOrderItemRepository : IRepositortyC <OrderItem, int, OrderItemResourceParameter>
    {
        public Task EditAsync(OrderItem entity);
        public Task<OrderItem> InsertByOrderIdAsync(int OrderId, OrderItem orderItem);
        public Task<IEnumerable<OrderItem>> FindOrderItemsofOrderIdAsync(int OrderId);
    }
}
