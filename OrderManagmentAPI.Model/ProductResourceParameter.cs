using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentAPI.Model
{
    public class ProductResourceParameter
    {
        public string SearchQuery { set; get; }

        public string Name { get; set; }

        public int InventoryItemId { get; set; }
    }
}
