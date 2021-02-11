using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentAPI.Service.Dto
{
    public class OrderForUpdateDto
    {

        public DateTime IssueDate { get; set; }
        public int ClientId { get; set; }

    }
}
