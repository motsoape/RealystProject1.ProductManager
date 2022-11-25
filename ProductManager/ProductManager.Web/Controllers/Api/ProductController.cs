using Microsoft.AspNetCore.Mvc;
using ProductManager.Repositories.Entities;
using ProductManager.Services;
using ProductManager.Services.Interfaces;
using ProductManager.Web.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics;

namespace ProductManager.Web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(IProductService productsService, ILogger<ProductController> logger)
        {
            _logger = logger;
            _productService = productsService;
        }

        // GET: api/Product
        [HttpGet]
        [SwaggerOperation("GetProducts")]
        public async Task<IEnumerable<Product>> Get()
        {
            IEnumerable<Product> product = await _productService.GetProducts();
            return product;
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        [SwaggerOperation("GetProduct")]
        public async Task<Product> Get(int id)
        {
            Product product = await _productService.GetProduct(id);

            return product;
        }

        // POST: api/Product
        [HttpPost]
        [SwaggerOperation("AddProduct")]
        public void Post([FromBody] Product product)
        {
            _productService.AddProduct(product);
        }

        // POST: api/Product/Bulk
        [HttpPost("bulk")]
        [SwaggerOperation("BulkAddProducts")]
        public void Post([FromBody] IEnumerable<Product> products)
        {
            _productService.AddBulkProduct(products);
        }

        // PUT: api/Product/{id}
        [HttpPut("{id}")]
        [SwaggerOperation("UpdateProduct")]
        public void Put(int id, [FromBody] Product product)
        {
            _productService.UpdateProduct(id, product);
        }

        // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation("DeleteProduct")]
        public void Delete(int id)
        {
            _productService.DeleteProduct(id);
        }
    }
}