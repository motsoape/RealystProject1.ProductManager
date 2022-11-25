using Microsoft.AspNetCore.Mvc;
using ProductManager.Repositories.Entities;
using ProductManager.Repositories.Models;
using ProductManager.Services;
using ProductManager.Services.Interfaces;
using ProductManager.Services.Models;
using ProductManager.Web.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics;

namespace ProductManager.Web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productsService)
        {
            _productService = productsService;
        }

        // GET: api/Product
        [HttpGet]
        [SwaggerOperation("GetProducts")]
        public async Task<IEnumerable<ProductModel>> Get()
        {
            IEnumerable<ProductModel> product = await _productService.GetProducts();
            return product;
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        [SwaggerOperation("GetProduct")]
        public async Task<ProductModel> Get(int id)
        {
            ProductModel product = await _productService.GetProduct(id);

            return product;
        }

        // POST: api/Product
        [HttpPost]
        [SwaggerOperation("AddProduct")]
        public async Task<bool> Post([FromBody] ProductModel product)
        {
            await _productService.AddProduct(product);

            return true;
        }

        // POST: api/Product/Bulk
        [HttpPost("bulk")]
        [SwaggerOperation("BulkAddProducts")]
        public async Task<bool> Post([FromBody] IEnumerable<ProductModel> products)
        {
            await _productService.AddBulkProduct(products);

            return true;
        }

        // PUT: api/Product/{id}
        [HttpPut("{id}")]
        [SwaggerOperation("UpdateProduct")]
        public async Task<bool> Put(int id, [FromBody] ProductModel product)
        {
            await _productService.UpdateProduct(id, product);

            return true;
        }

        // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation("DeleteProduct")]
        public async Task<bool> Delete(int id)
        {
            await _productService.DeleteProduct(id);

            return true;
        }
    }
}