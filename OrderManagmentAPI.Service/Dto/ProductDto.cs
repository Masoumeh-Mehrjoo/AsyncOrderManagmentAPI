using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentAPI.Service.Dto
{
    public class ProductDto
    {
        public int id { get; set; }
        public string Name { get; set; }
        public float CurrentPrice { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public int InventoryItemId { get; set; }
    }
}
