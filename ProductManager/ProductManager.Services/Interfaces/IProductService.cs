using ProductManager.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Services.Interfaces
{
    public interface IProductService
    {
        void AddBulkProduct(IEnumerable<Product> products);
        void AddProduct(Product product);
        void DeleteProduct(Product product);
        Task<Product> GetProduct(int id);
        Task<IEnumerable<Product>> GetProducts();
        void UpdateProduct(Product product);
    }
}
