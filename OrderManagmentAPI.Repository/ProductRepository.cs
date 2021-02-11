using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public void Insert(Product entity)
        {
            _context.Products.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Product Edit(int Id)
        {
            throw new NotImplementedException();
        }

        public Product findbyId(int Id)
        {
            //  if ((Id == 0) || (_context.Products.Find(Id) == null))
            //  {
            //       throw new ArgumentNullException(nameof(Id));
            //  }

            return _context.Products.Find(Id);
        }

        public IEnumerable<Product> AllRows()
        {
            return _context.Products.ToList();
        }


        public IEnumerable<Product> SearchedRows(ProductResourceParameter parameter)
        {
            string searchQuery = parameter.SearchQuery;

            IQueryable<Product> Products = _context.Products;

            if (!(parameter.InventoryItemId == 0))
            {
                Products = Products.Where(a => (a.InventoryItemId == parameter.InventoryItemId));
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                Products = Products.Where(a => a.Name.Contains(searchQuery) ||
                a.Description.Contains(searchQuery) || (a.CurrentPrice.ToString() == searchQuery) ||
                a.SKU.Contains(searchQuery)
                );
            }

            return Products.ToList();

        }
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Edit(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
