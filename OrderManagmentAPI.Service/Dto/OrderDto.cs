using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentAPI.Service.Dto
{
    public class OrderDto
    {
        public int id { get; set; }
         public DateTime IssueDate { get; set; }
        public float Tax { get; set; }
        public int Count { get; set; }
        public float TotalPrice { get; private set; }
        public float FinalPrice { get; set; }
    }
}
