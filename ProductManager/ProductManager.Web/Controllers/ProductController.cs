using Microsoft.AspNetCore.Mvc;
using ProductManager.Repositories.Entities;
using ProductManager.Services;
using ProductManager.Services.Interfaces;
using ProductManager.Web.Models;
using System.Diagnostics;

namespace ProductManager.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductController : Controller
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
        public async Task<IActionResult> Get()
        {
            IEnumerable<Product> product = await _productService.GetProducts();
            return Ok(product);
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Product product = await _productService.GetProduct(id);
            if (product == null)
            {
                return NotFound("The comment record couldn't be found.");
            }
            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Comment is null.");
            }
            _productService.AddProduct(product);

            return CreatedAtRoute("Get", new { Id = product.ProductID }, product);
        }

        // POST: api/Product/Bulk
        [HttpPost]
        public IActionResult Post([FromBody] IEnumerable<Product> products)
        {
            if (products == null)
            {
                return BadRequest("Comments is null.");
            }
            _productService.AddBulkProduct(products);

            return NoContent();
        }

        // PUT: api/Product/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product is null.");
            }

            _productService.UpdateProduct(id, product);

            return NoContent();
        }

        // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);

            return NoContent();
        }
    }
}