using OrderManagmentAPI.Model;
using OrderManagmentAPI.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public IEnumerable<Order> AllRows()
        {
            return _context.Orders.ToList();
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrderItem(Order entity, OrderItem orderItem)
        {
            entity = entity.DeleteOrderItem(orderItem);
            _context.Orders.Update(entity);
            _context.SaveChanges();
        }

        public void AddNewOrderItem(Order entity, OrderItem orderItem)
        {
            entity = entity.AddNewOrderItem(orderItem);

            _context.Orders.Update(entity);
            _context.SaveChanges();

        }

        public Order findbyId(int Id)
        {
            return _context.Orders.Find(Id);
        }

        public void Insert(Order entity)
        {
            foreach (var orderitem in entity.OrderItems)
                entity = entity.AddOrderItemInCreationOrder(orderitem);

            _context.Orders.Add(entity);
            _context.SaveChanges();

        }

        public void InsertOrderWithOrderItem(Order order)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public IEnumerable<Order> SearchedRows(OrderResourceParameter parameter)
        {
            throw new NotImplementedException();
        }

        public void Edit(Order entity)
        {
            
        }

        public void EditOrderItem(Order entity, OrderItem OldOrderItem, OrderItem NewOrderItem)
        {
            entity = entity.EditOrderItem(OldOrderItem, NewOrderItem);

            _context.Orders.Update(entity);
            _context.SaveChanges();

        }
    }
}
