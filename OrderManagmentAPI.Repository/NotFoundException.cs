using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentAPI.Repository
{
    public class NotFoundException : Exception
    {
        private string v;

        public NotFoundException()
        {

        }
        public NotFoundException(string message, string v) : base(message)
        {
            //   this.v = v;
            // }
        }
    }
}
