using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderManagmentAPI.Model;
using OrderManagmentAPI.Model.Repository;

namespace OrderManagmentAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private OrderContext _context;
        public ProductRepository(OrderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task InsertAsync(Product entity)
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }


        public Product Edit(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> findbyIdAsync(int Id)
        {
            //  if ((Id == 0) || (_context.Products.Find(Id) == null))
            //  {
            //       throw new ArgumentNullException(nameof(Id));
            //  }

            return await _context.Products.FindAsync(Id);
        }

        public async Task<IEnumerable<Product>> AllRowsAsync()
        {
            return await _context.Products.ToListAsync();
        }


        public async Task<IEnumerable<Product>> SearchedRowsAsync(ProductResourceParameter parameter)
        {
            string searchQuery = parameter.SearchQuery;

            var productTask = Task.Run(() => _context.Products);
            IQueryable<Product> Products = await productTask;

            if (!(parameter.InventoryItemId == 0))
            {
                Products = await Task.Run(() => Products.Where(a => (a.InventoryItemId == parameter.InventoryItemId)));
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                Products = await Task.Run(() => Products.Where(a => a.Name.Contains(searchQuery) ||
                a.Description.Contains(searchQuery) || (a.CurrentPrice.ToString() == searchQuery) ||
                a.SKU.Contains(searchQuery)));
            }

            return await Products.ToListAsync();

        }
        public async Task<bool> Save()
        {
            var Result = await _context.SaveChangesAsync();
            return (Result >= 0);

        }
        public void Edit(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
