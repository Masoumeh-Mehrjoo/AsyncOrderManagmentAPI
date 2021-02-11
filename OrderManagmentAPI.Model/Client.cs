using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentAPI.Model
{
    public class Client : EntityBase<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int CRMId { get; set; }

    }
}
