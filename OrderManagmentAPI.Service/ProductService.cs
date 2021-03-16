using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using OrderManagmentAPI.Model;
using OrderManagmentAPI.Model.Repository;
using OrderManagmentAPI.Repository;
using OrderManagmentAPI.Service.Dto;
using OrderManagmentAPI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentAPI.Service
{
    public class ProductService : IProductService

    {
        IProductRepository _iProductRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this._iProductRepository = productRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ProductDto> AddNewProductAsync(ProductDtoForCreation productDtoForCreation)
        {
            try
            {
                var Product = _mapper.Map<Product>(productDtoForCreation);
                await _iProductRepository.InsertAsync(Product);

                var productDto = _mapper.Map<ProductDto>(Product);
                return  productDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            try
            {
                var products = await _iProductRepository.AllRowsAsync();

                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

                return (productDtos);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ProductDto> FindByIdAsync(int Id)
        {
            var product = await _iProductRepository.findbyIdAsync(Id);
            var productDto = _mapper.Map<ProductDto>(product);
            return (productDto);

        }

        public async Task<IEnumerable<ProductDto>> SearchedRowsAsync(ProductResourceParameter productResourceParameter)
        {
            var products = await _iProductRepository.SearchedRowsAsync(productResourceParameter);
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return (productDtos);
        }
        public async Task DeleteProductAsync(int Id)
        {
            await _iProductRepository.DeleteAsync(Id);
        }

        public async Task PatchProductAsync(int Id, JsonPatchDocument<ProductDtoForUpdate> patchDocument)
        {
            try
            {
                var Product = await _iProductRepository.findbyIdAsync(Id);

                var ProductForUpdateDto = _mapper.Map<ProductDtoForUpdate>(Product);
                patchDocument.ApplyTo(ProductForUpdateDto);

                _mapper.Map(ProductForUpdateDto, Product);
                _iProductRepository.Edit(Product);

                await _iProductRepository.SaveAsync();
            }
            catch (Exception)
            {
                throw new NotFoundException();
            }
        }

        public async Task EditProductAsync(int Id, ProductDtoForUpdate product)
        {
            try
            {
                var Product = await _iProductRepository.findbyIdAsync(Id);

               // var ProductForUpdateDto = _mapper.Map<ProductDtoForUpdate>(Product);
               
                _mapper.Map(product, Product);
                _iProductRepository.Edit(Product);

                await _iProductRepository.SaveAsync();
            }
            catch (Exception)
            {
                throw new NotFoundException();
            }
        }
    }
}



