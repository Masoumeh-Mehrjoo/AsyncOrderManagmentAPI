using Microsoft.AspNetCore.JsonPatch;
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

        public ProductControllerAsync(IProductService productService)
        {
            _ProductService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct(ProductDtoForCreation product)
        {
            var ProductToReturn = await _ProductService.AddNewProductAsync(product);
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

            return new JsonResult(ProductDtos);

        }

        [HttpGet("{id}", Name = "GetProductBYId")]
        public async Task<ActionResult> GetProductBYId(int Id)
        {
            var Product = await _ProductService.FindByIdAsync(Id);

            if (Product == null)
            {
                return NotFound("This Product Id is not exist in database.");
            }

            return new JsonResult(Product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int Id)
        {
            var Client = await _ProductService.FindByIdAsync(Id);
            if (Client == null)
            {
                return NotFound("This Client Id does not exist.");

            }
            await _ProductService.DeleteProductAsync(Id);
            return NoContent();

        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatriallyUpdateProduct(int Id, JsonPatchDocument<ProductDtoForUpdate> patchDocument)
        {
            try
            {
                await _ProductService.PatchProductAsync(Id, patchDocument);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound("Jason parameters are not Correct or this Client Id does not exist.");
            }
        }
    
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int Id, ProductDtoForUpdate product)
        {
            try
            {
                await _ProductService.EditProductAsync(Id, product);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound("Jason parameters are not Correct or this Client Id does not exist.");
            }
        }
    }
}
