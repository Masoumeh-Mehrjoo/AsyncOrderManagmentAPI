using OrderManagmentAPI.Model;
using OrderManagmentAPI.Model.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace OrderManagmentAPI.Repository
{
    public class OrderItemRepository : IOrderItemRepository

    {
        private OrderContext _context;
        public OrderItemRepository(OrderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task DeleteAsync(int Id)
        {
            var OrderItem = await _context.OrderItems.Where(x => x.id == Id).FirstOrDefaultAsync();

            _context.OrderItems.Remove(OrderItem);

            var OrderRepository = new OrderRepository(_context);
            var UpdateOrder = await OrderRepository.findbyIdAsync(OrderItem.OrderId);

            await OrderRepository.DeleteOrderItemAsync(UpdateOrder, OrderItem);

            _context.SaveChanges();
        }

        public async Task EditAsync(OrderItem entity)
        {
            var OldOrderItem = _context.OrderItems.AsNoTracking().Single(x => x.id == entity.id);

            var OrderRepository = new OrderRepository(_context);
            var UpdateOrder = await OrderRepository.findbyIdAsync(entity.OrderId);
            entity = new OrderItem(entity.OrderId, entity.ProductId, entity.SoldPrice, entity.Count);

            await OrderRepository.EditOrderItemAsync(UpdateOrder, OldOrderItem, entity);

            _context.OrderItems.Update(entity);
        }

        public async Task<OrderItem> findbyIdAsync(int Id)
        {
            return await _context.OrderItems.FindAsync(Id);
        }

        public async Task<IEnumerable<OrderItem>> FindOrderItemsofOrderIdAsync(int OrderId)
        {
            return await _context.OrderItems.Where(x => x.OrderId == OrderId).ToListAsync();
        }

        public async Task InsertAsync(OrderItem entity)
        {
            _context.OrderItems.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<OrderItem> InsertByOrderIdAsync(int OrderId, OrderItem entity)
        {
            entity.OrderId = OrderId;
            await InsertAsync(entity);

            var OrderRepository = new OrderRepository(_context);
            var UpdateOrder = await OrderRepository.findbyIdAsync(OrderId);


            await OrderRepository.AddNewOrderItemAsync(UpdateOrder, entity);
            return (entity);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);

        }
        public void Edit(OrderItem entity)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<OrderItem>> SearchedRowsAsync(OrderItemResourceParameter parameter)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<OrderItem>> AllRowsAsync()
        {
            throw new NotImplementedException();
        }

    }
}
