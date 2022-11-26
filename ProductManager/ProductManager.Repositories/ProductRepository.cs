using Microsoft.EntityFrameworkCore;
using ProductManager.Repositories.Entities;
using ProductManager.Repositories.Interfaces;
using ProductManager.Repositories.Models;

namespace ProductManager.Repositories
{
    public class ProductRepository : IDataRepository<ProductModel>
    {
        readonly ProductManagerDbContext _context;
        public ProductRepository(ProductManagerDbContext context)
        {
            _context = context;
        }

        public async Task Add(ProductModel entity)
        {
            var newEntity = new Product
            {
                Name= entity.Name,
                Price= entity.Price,
                ReleaseDate= entity.ReleaseDate
            };
            await _context.Products.AddAsync(newEntity);
            await _context.SaveChangesAsync();
        }

        public async Task AddBulk(IEnumerable<ProductModel> entities)
        {
            var newEntities = new List<Product>();
            foreach (var entity in entities)
            {
                newEntities.Add(new Product
                {
                    Name = entity.Name,
                    Price = entity.Price,
                    ReleaseDate = entity.ReleaseDate
                });
            }
            await _context.Products.AddRangeAsync(newEntities);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(ProductModel entity)
        {
            var entityToDelete = await _context.Products.FirstOrDefaultAsync(e => e.ProductID == entity.ProductID);

            if (entityToDelete == null)
                throw new Exception("The product cannot be found");

            _context.Products.Remove(entityToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductModel> Get(int id)
        {
            return await _context.Products.Include(x => x.Comments)
                .Select(x => new ProductModel
                {
                    Name = x.Name,
                    ProductID = x.ProductID,
                    Price = x.Price,
                    ReleaseDate = x.ReleaseDate,
                    Comments = x.Comments.Select(c => new CommentModel
                    {
                        CommentID = c.CommentID,
                        CommentContent = c.CommentContent,
                        DateOfComment = c.DateOfComment,
                        Email = c.Email,
                        ProductID = c.ProductID
                    })
                }).FirstOrDefaultAsync(e => e.ProductID == id);
        }

        public async Task<IEnumerable<ProductModel>> GetAll()
        {
            return await _context.Products.Include(x => x.Comments)
                .Select(x => new ProductModel
                {
                    Name = x.Name,
                    ProductID = x.ProductID,
                    Price = x.Price,
                    ReleaseDate = x.ReleaseDate,
                    Comments = x.Comments.Select(c => new CommentModel
                    {
                        CommentID = c.CommentID,
                        CommentContent = c.CommentContent,
                        DateOfComment = c.DateOfComment,
                        Email = c.Email,
                        ProductID = c.ProductID
                    })
                }).ToListAsync();
        }

        public async Task Update(int id, ProductModel newEntity)
        {
            if (newEntity == null)
                throw new Exception("The product cannot be null");

            var oldEntity = await _context.Products.FirstOrDefaultAsync(e => e.ProductID == id);

            if (oldEntity == null)
                throw new Exception("The product cannot be found");

            oldEntity.Price = newEntity.Price;
            oldEntity.Name = newEntity.Name;
            oldEntity.ReleaseDate = newEntity.ReleaseDate;

            await _context.SaveChangesAsync();
        }
    }
}