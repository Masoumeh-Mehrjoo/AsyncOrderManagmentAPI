using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentAPI.Model
{
    public class ClientResourceParameter
    {
        public string SearchQuery { set; get; }

        public string FirstName { get; set; }
        public int CRMId { get; set; }

    }
}
