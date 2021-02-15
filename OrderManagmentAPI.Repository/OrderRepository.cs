using Microsoft.EntityFrameworkCore;
using OrderManagmentAPI.Model;
using OrderManagmentAPI.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private OrderContext _context;

        OrderItemRepository _orderItemRepository;
        public OrderRepository(OrderContext orderContext)
        {
            _context = orderContext;
            _orderItemRepository = new OrderItemRepository(_context);

        }
        public async Task<IEnumerable<Order>> AllRowsAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        
        public async Task DeleteOrderItemAsync(Order entity, OrderItem orderItem)
        {
            entity = entity.DeleteOrderItem(orderItem);
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddNewOrderItemAsync(Order entity, OrderItem orderItem)
        {
            entity = entity.AddNewOrderItem(orderItem);

            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();

        }

        public async Task<Order> findbyIdAsync(int Id)
        {
            return await _context.Orders.FindAsync(Id);
        }

        public async Task InsertAsync(Order entity)
        {
            foreach (var orderitem in entity.OrderItems)
                entity = entity.AddOrderItemInCreationOrder(orderitem);

            _context.Orders.Add(entity);
            await _context.SaveChangesAsync();

        }

        
        

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        
        public void Edit(Order entity)
        {

        }

        public async Task EditOrderItemAsync(Order entity, OrderItem OldOrderItem, OrderItem NewOrderItem)
        {
            entity = entity.EditOrderItem(OldOrderItem, NewOrderItem);

            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();

        }
        public async Task<IEnumerable<Order>> SearchedRowsAsync(OrderResourceParameter parameter)
        {
            throw new NotImplementedException();
        }
        public async Task InsertOrderWithOrderItemAsync(Order order)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteAsync(int Id)
        {
            throw new NotImplementedException();
        }


    }
}
