using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentAPI.Model.Repository
{
    public interface IProductRepository : IRepository<Product,int,ProductResourceParameter>
    {

    }
}
