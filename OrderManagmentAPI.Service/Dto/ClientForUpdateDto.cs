﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentAPI.Service.Dto
{
    public class ClientForUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int CRMId { get; set; }
    }
}
