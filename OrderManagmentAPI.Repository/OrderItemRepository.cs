using OrderManagmentAPI.Model;
using OrderManagmentAPI.Model.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OrderManagmentAPI.Repository
{
    public class OrderItemRepository : IOrderItemRepository

    {
        private OrderContext _context;
        public OrderItemRepository(OrderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IEnumerable<OrderItem> AllRows()
        {
            throw new NotImplementedException();
        }
        public void Delete(int Id)
        {
            var OrderItem = _context.OrderItems.Where(x => x.id == Id).FirstOrDefault();
            _context.OrderItems.Remove(OrderItem);

            var OrderRepository = new OrderRepository(_context);
            var UpdateOrder = OrderRepository.findbyId(OrderItem.OrderId);

            OrderRepository.DeleteOrderItem(UpdateOrder, OrderItem);

            _context.SaveChanges();
        }

        public void Edit(OrderItem entity)
        {
            var OldOrderItem = _context.OrderItems.AsNoTracking().Single(x => x.id == entity.id);

            var OrderRepository = new OrderRepository(_context);
            var UpdateOrder = OrderRepository.findbyId(entity.OrderId);
            entity = new OrderItem(entity.OrderId, entity.ProductId, entity.SoldPrice, entity.Count);

            OrderRepository.EditOrderItem(UpdateOrder, OldOrderItem, entity);

            _context.OrderItems.Update(entity);
        }

        public OrderItem findbyId(int Id)
        {
            return _context.OrderItems.Find(Id);
        }

        public IEnumerable<OrderItem> FindOrderItemsofOrderId(int OrderId)
        {
            return _context.OrderItems.Where(x => x.OrderId == OrderId);
        }

        public void Insert(OrderItem entity)
        {
            _context.OrderItems.Add(entity);
            _context.SaveChanges();
        }

        public OrderItem InsertByOrderId(int OrderId, OrderItem entity)
        {
            entity.OrderId = OrderId;
            Insert(entity);

            var OrderRepository = new OrderRepository(_context);
            var UpdateOrder = OrderRepository.findbyId(OrderId);


            OrderRepository.AddNewOrderItem(UpdateOrder, entity);
            return (entity);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);

        }
        public IEnumerable<OrderItem> SearchedRows(OrderItemResourceParameter parameter)
        {
            throw new NotImplementedException();
        }
    }
}
