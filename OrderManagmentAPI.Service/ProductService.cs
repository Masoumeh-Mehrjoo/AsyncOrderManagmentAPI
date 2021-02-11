using AutoMapper;
using OrderManagmentAPI.Model;
using OrderManagmentAPI.Model.Repository;
using OrderManagmentAPI.Repository;
using OrderManagmentAPI.Service.Dto;
using OrderManagmentAPI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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

        public ProductDto AddNewProduct(ProductDtoForCreation productDtoForCreation)
        {
            try
            {
                var Product = _mapper.Map<Product>(productDtoForCreation);
                _iProductRepository.Insert(Product);

                var productDto = _mapper.Map<ProductDto>(Product);
                return productDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ProductDto> GetAll()
        {
            try
            {
                var products = _iProductRepository.AllRows();

                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

                return (productDtos);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ProductDto FindById(int Id)
        {
            var product = _iProductRepository.findbyId(Id);
            var productDto = _mapper.Map<ProductDto>(product);
            return (productDto);

        }

        public IEnumerable<ProductDto> SearchedRows(ProductResourceParameter productResourceParameter)
        {
            var products = _iProductRepository.SearchedRows(productResourceParameter);
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return (productDtos);
        }
    }
}



