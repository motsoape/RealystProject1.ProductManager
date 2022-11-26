using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productsService, ILogger<ProductController> logger)
        {
            _productService = productsService;
            _logger = logger;
        }

        // GET: api/Product
        [HttpGet]
        [SwaggerOperation("GetProducts")]
        public async Task<IEnumerable<ProductModel>> Get()
        {
            try { 
                IEnumerable<ProductModel> product = await _productService.GetProducts();
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Get Products", ex);
                throw;
            }
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        [SwaggerOperation("GetProduct")]
        public async Task<ProductModel> Get(int id)
        {
            try { 
                ProductModel product = await _productService.GetProduct(id);

                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Get Product", ex);
                throw;
            }
        }

        // POST: api/Product
        [HttpPost]
        [SwaggerOperation("AddProduct")]
        public async Task<bool> Post([FromBody] ProductModel product)
        {
            try {
                if (product == null)
                    throw new ArgumentNullException(nameof(product), "Cannot be Null or Empty");

                await _productService.AddProduct(product);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Add Product", ex);
                throw;
            }
        }

        // POST: api/Product/Bulk
        [HttpPost("bulk")]
        [SwaggerOperation("BulkAddProducts")]
        public async Task<bool> Post([FromBody] IEnumerable<ProductModel> products)
        {
            try {
                if (products == null)
                    throw new ArgumentNullException(nameof(products), "Cannot be Null or Empty");

                await _productService.AddBulkProduct(products);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Add Products", ex);
                throw;
            }
        }

        // PUT: api/Product/{id}
        [HttpPut("{id}")]
        [SwaggerOperation("UpdateProduct")]
        public async Task<bool> Put(int id, [FromBody] ProductModel product)
        {
            try {
                if (product == null)
                    throw new ArgumentNullException(nameof(product), "Cannot be Null or Empty");

                await _productService.UpdateProduct(id, product);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Update Product", ex);
                throw;
            }
        }

        // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation("DeleteProduct")]
        public async Task<bool> Delete(int id)
        {
            try { 
                await _productService.DeleteProduct(id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Update Product", ex);
                throw;
            }
        }
    }
}