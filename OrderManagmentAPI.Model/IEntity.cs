using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentAPI.Model
{
    interface IEntity<T>
    {
        T id { get; set; }
    }
}
