using ProductManager.Repositories.Interfaces;
using ProductManager.Repositories.Models;
using ProductManager.Services.Interfaces;

namespace ProductManager.Services
{
    public class ProductService : IProductService
    {
        private readonly IDataRepository<ProductModel> _productRepository;

        public ProductService(IDataRepository<ProductModel> productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<ProductModel> GetProduct(int id) 
        {
            return _productRepository.Get(id);
        }

        public Task<IEnumerable<ProductModel>> GetProducts()
        {
            return _productRepository.GetAll();
        }

        public async Task AddProduct(ProductModel product)
        {
            if (product == null)
                throw new ArgumentException("Product cannot be null");

            await _productRepository.Add(product);
        }

        public async Task AddBulkProduct(IEnumerable<ProductModel> products)
        {
            if (products == null)
                throw new ArgumentException("Product cannot be null");

            await _productRepository.AddBulk(products);
        }

        public async Task UpdateProduct(int productId, ProductModel newProduct)
        {
            if (newProduct == null)
                throw new ArgumentException("New new Product cannot be null");

            await _productRepository.Update(productId, newProduct);
        }

        public async Task DeleteProduct(int productId)
        {
            var product = await GetProduct(productId);
            if (product == null)
                throw new Exception("Product not found");    

            await _productRepository.Delete(product);
        }
    }
}