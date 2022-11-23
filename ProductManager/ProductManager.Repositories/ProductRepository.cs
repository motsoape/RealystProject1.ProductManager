using Microsoft.EntityFrameworkCore;
using ProductManager.Repositories.Entities;
using ProductManager.Repositories.Interfaces;

namespace ProductManager.Repositories
{
    public class ProductRepository : IDataRepository<Product>
    {
        readonly ProductContext _productContext;
        public ProductRepository(ProductContext context)
        {
            _productContext = context;
        }

        public async void Add(Product entity)
        {
            await _productContext.Products.AddAsync(entity);
            await _productContext.SaveChangesAsync();
        }

        public async void AddBulk(IEnumerable<Product> entities)
        {
            await _productContext.Products.AddRangeAsync(entities);
            await _productContext.SaveChangesAsync();
        }

        public async void Delete(Product entity)
        {
            _productContext.Products.Remove(entity);
            await _productContext.SaveChangesAsync();
        }

        public async Task<Product> Get(int id)
        {
            return await _productContext.Products.FirstOrDefaultAsync(e => e.ProductID == id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productContext.Products.ToListAsync();
        }

        public async void Update(Product entity)
        {
            var _entityToUpdate = await Get(entity.ProductID);
            if (_entityToUpdate != null)
                throw new Exception("The product cannot be updated");

            _entityToUpdate.Price = entity.Price;
            _entityToUpdate.Name = entity.Name;
            _entityToUpdate.ReleaseDate = entity.ReleaseDate;

            await _productContext.SaveChangesAsync();
        }
    }
}