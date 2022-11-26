using ProductManager.Repositories.Entities;
using ProductManager.Repositories.Models;
using ProductManager.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Services.Interfaces
{
    public interface IProductService
    {
        Task AddBulkProduct(IEnumerable<ProductModel> products);
        Task AddProduct(ProductModel product);
        Task DeleteProduct(int productId);
        Task<ProductModel> GetProduct(int id);
        Task<IEnumerable<ProductModel>> GetProducts();
        Task UpdateProduct(int productId, ProductModel newProduct);
    }
}
