using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentAPI.Model
{
    public class Product : EntityBase<int>
    {
        public float CurrentPrice { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int InventoryItemId { get; set; }

    }
}
