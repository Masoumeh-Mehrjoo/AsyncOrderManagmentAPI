using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentAPI.Model
{
    public abstract class EntityBase<T> : IEntity<T>
    {
        public T id { get; set; }
    }
}
