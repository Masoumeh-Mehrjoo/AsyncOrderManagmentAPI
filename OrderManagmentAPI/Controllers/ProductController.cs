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

    public class ProductController : ControllerBase
    {
        readonly IProductService _ProductService;

        public ProductController(IProductService productService)
        {
            _ProductService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpPost]
        public IActionResult PostProduct(ProductDtoForCreation product)
        {
            var ProductToReturn = _ProductService.AddNewProduct(product);
            return CreatedAtRoute("GetProductBYId", new { Id = ProductToReturn.id }, ProductToReturn);
        }

        [HttpGet()]
        public ActionResult GetProducts([FromQuery] ProductResourceParameter productResourceParameter)
        {
            IEnumerable<ProductDto> ProductDtos;

            if (productResourceParameter == null)
            {
                ProductDtos = _ProductService.GetAll();
            }
            else
            {
                ProductDtos = _ProductService.SearchedRows(productResourceParameter);
            }

            return Ok(ProductDtos);
        }

        [HttpGet("{id}", Name = "GetProductBYId")]
        public ActionResult GetProductBYId(int Id)
        {
            var Product = _ProductService.FindById(Id);

            if (Product == null)
            {
                return NotFound("This Product Id is not exist in database.");
            }

            return Ok(Product);
        }
    }
}
