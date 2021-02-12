using OrderManagmentAPI.Model;
using OrderManagmentAPI.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentAPI.Service.Interfaces
{
    public interface IProductService
    {
        public Task<ProductDto> AddNewProductAsync(ProductDtoForCreation product);

        public Task<IEnumerable<ProductDto>> GetAllAsync();

        public Task<ProductDto> FindByIdAsync(int Id);

        public Task<IEnumerable<ProductDto>> SearchedRowsAsync(ProductResourceParameter  productResourceParameter);

    }
}
