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

    public class ClientRepository : IClientRepository
    {
        private OrderContext _context;
        public ClientRepository(OrderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Client>> AllRowsAsync()
        {
            return await _context.Client.ToListAsync();
        }
        public async Task DeleteAsync(int Id)
        {
            var client = await _context.Client.Where(x => x.id == Id).FirstOrDefaultAsync();
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
        }
        public void Edit(Client entity)
        {

        }
        public async Task<Client> findbyIdAsync(int Id)
        {
            var client = await _context.Client.Where(x => x.id == Id).FirstOrDefaultAsync();
            return client;
        }
        public async Task InsertAsync(Client entity)
        {
            _context.Client.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Client>> SearchedRowsAsync(ClientResourceParameter parameter)
        {
            string searchQuery = parameter.SearchQuery;

            var ClientTask = Task.Run(() => _context.Client);
            IQueryable<Client> Client = await ClientTask;


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

            return await Client.ToListAsync();

        }
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
        public async Task<List<Order>> OrdersOfClientAsync(int clientId)
        {
            var orders = await _context.Orders.Where(c => c.client.id == clientId).ToListAsync();
            return orders;

        }
    }
}

