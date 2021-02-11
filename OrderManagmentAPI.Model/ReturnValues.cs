using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentAPI.Model
{
    public class ReturnValues
    {
        public OrderDto RetOrder;
        public IEnumerable<OrderItemDto> RetorderItems;
        public int s;
    }

}
