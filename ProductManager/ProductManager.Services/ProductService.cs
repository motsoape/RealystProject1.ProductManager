using ProductManager.Repositories.Entities;
using ProductManager.Repositories.Interfaces;
using ProductManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Services
{
    public class ProductService : IProductService
    {
        private readonly IDataRepository<Product> _productRepository;

        public ProductService(IDataRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<Product> GetProduct(int id) 
        {
            return _productRepository.Get(id);
        }

        public Task<IEnumerable<Product>> GetProducts()
        {
            return _productRepository.GetAll();
        }

        public void AddProduct(Product product)
        {
            if (product == null)
                throw new ArgumentException("Product cannot be null");

            _productRepository.Add(product);
        }

        public void AddBulkProduct(IEnumerable<Product> products)
        {
            if (products == null)
                throw new ArgumentException("Product cannot be null");

            _productRepository.AddBulk(products);
        }

        public void UpdateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentException("Product cannot be null");

            _productRepository.Update(product);
        }

        public void DeleteProduct(Product product)
        {
            if (product == null)
                throw new ArgumentException("Product cannot be null");    

            _productRepository.Delete(product);
        }
    }
}