using OrderManagmentAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentAPI.Service.Dto
{
    public class OrderForCreationDto
    {
        public DateTime IssueDate { get; set; }
        public int clientId { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }

    }
}
