using Microsoft.EntityFrameworkCore;
using ProductManager.Repositories.Entities;
using ProductManager.Repositories.Interfaces;
using System.Security.Principal;

namespace ProductManager.Repositories
{
    public class ProductRepository : IDataRepository<Product>
    {
        readonly ProductManagerDbContext _context;
        public ProductRepository(ProductManagerDbContext context)
        {
            _context = context;
        }

        public async void Add(Product entity)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async void AddBulk(IEnumerable<Product> entities)
        {
            await _context.Products.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async void Delete(Product entity)
        {
            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> Get(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(e => e.ProductID == id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async void Update(Product oldEntity, Product newEntity)
        {
            if (oldEntity == null || newEntity == null)
                throw new Exception("The product cannot be updated");

            oldEntity.Price = newEntity.Price;
            oldEntity.Name = newEntity.Name;
            oldEntity.ReleaseDate = newEntity.ReleaseDate;

            await _context.SaveChangesAsync();
        }
    }
}