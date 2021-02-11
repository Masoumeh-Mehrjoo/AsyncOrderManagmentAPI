using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentAPI.Model.Repository
{
    public interface IClientRepository:IRepository<Client,int,ClientResourceParameter>
    {
        public List<Order> OrdersOfClient(int clientId);
    }

      
}
