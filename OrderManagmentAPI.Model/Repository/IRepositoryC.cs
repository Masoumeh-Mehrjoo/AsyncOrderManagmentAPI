using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentAPI.Model.Repository
{
    public interface IRepositortyC<T, J, S>
    {
        public Task InsertAsync(T entity);
        public Task DeleteAsync(J Id);
        public void Edit(T entity);
        public Task<T> findbyIdAsync(J Id);
        public Task<IEnumerable<T>> AllRowsAsync();
        public Task<IEnumerable<T>> SearchedRowsAsync(S parameter);
        public Task<bool> Save();

    }
}
