using OrderManagmentAPI.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagmentAPI
{
        public class ReturnValues
        {
            public OrderDto RetOrder;
            public IEnumerable<OrderItemDto> RetorderItems;
            public int s;
        }

}
