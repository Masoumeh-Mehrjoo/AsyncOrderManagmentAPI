using Microsoft.EntityFrameworkCore;
using OrderManagmentAPI.Model;
using OrderManagmentAPI.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderManagmentAPI.Repository
{

    public class ClientRepository : IClientRepository
    {
        private OrderContext _context;
        public ClientRepository(OrderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IEnumerable<Client> AllRows()
        {
            return _context.Client.ToList();
        }

        public void Delete(int Id)
        {
            var client = _context.Client.Where(x => x.id == Id).FirstOrDefault();
            _context.Client.Remove(client);
            _context.SaveChanges();

        }

        public void Edit(Client entity)
        {

        }

        public Client findbyId(int Id)
        {

            var client = _context.Client.Where(x => x.id == Id).FirstOrDefault();
            return client;
        }

        public void Insert(Client entity)
        {
            _context.Client.Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Client> SearchedRows(ClientResourceParameter parameter)
        {
            string searchQuery = parameter.SearchQuery;

            IQueryable<Client> Client = _context.Client;

            if (!(parameter.CRMId == 0))
            {
                Client = Client.Where(a => (a.CRMId == parameter.CRMId));
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                Client = Client.Where(a => a.FirstName.Contains(searchQuery) ||
                a.LastName.Contains(searchQuery)
                );
            }

            return Client.ToList();

        }
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public List<Order> OrdersOfClient(int clientId)
        {
            var orders = _context.Orders.Where(c => c.client.id == clientId).ToList();
            return (orders);
        }
    }
}

