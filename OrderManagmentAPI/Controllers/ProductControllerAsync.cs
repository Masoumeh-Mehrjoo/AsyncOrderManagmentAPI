using Microsoft.AspNetCore.Mvc;
using OrderManagmentAPI.Model;
using OrderManagmentAPI.Repository;
using OrderManagmentAPI.Service;
using OrderManagmentAPI.Service.Dto;
using OrderManagmentAPI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagmentAPI.Controllers
{
    [Route("api/Product")]
    [ApiController]

    public class ProductControllerAsync : ControllerBase
    {
        readonly IProductService _ProductService;

        public  ProductControllerAsync(IProductService productService)
        {
            _ProductService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpPost]
        public async  Task<IActionResult> PostProduct(ProductDtoForCreation product)
        {
            var ProductToReturn =  await _ProductService.AddNewProductAsync(product);
            return CreatedAtRoute("GetProductBYId", new { Id = ProductToReturn.id }, ProductToReturn);
        }

        [HttpGet()]
        public async Task<ActionResult> GetProducts([FromQuery] ProductResourceParameter productResourceParameter)
        {
            IEnumerable<ProductDto> ProductDtos;

            if (productResourceParameter == null)
            {
                ProductDtos = await _ProductService.GetAllAsync();
            }
            else
            {
                ProductDtos = await _ProductService.SearchedRowsAsync(productResourceParameter);
            }

            return Ok(ProductDtos);
        }

        [HttpGet("{id}", Name = "GetProductBYId")]
        public async Task<ActionResult> GetProductBYId(int Id)
        {
            var Product = await _ProductService.FindByIdAsync(Id);

            if (Product == null)
            {
                return NotFound("This Product Id is not exist in database.");
            }

            return Ok(Product);
        }
    }
}
