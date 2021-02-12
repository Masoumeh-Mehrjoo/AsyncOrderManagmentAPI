using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentAPI.Model.Repository
{
    public interface IClientRepository:IRepositortyC<Client,int,ClientResourceParameter>
    {
        //public List<Order> OrdersOfClient(int clientId);

        public Task<List<Order>> OrdersOfClientAsync(int clientId);
    }

      
}
