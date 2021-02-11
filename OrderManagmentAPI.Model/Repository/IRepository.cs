using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentAPI.Model.Repository
{
    public interface  IRepository<T,J,S>
    {
        public void Insert(T entity);
        public void Delete(J Id);
        public void Edit(T entity);
        public T findbyId(J Id);

        public IEnumerable<T> AllRows();
        public IEnumerable<T> SearchedRows(S parameter );
        public bool Save();


    }
}
