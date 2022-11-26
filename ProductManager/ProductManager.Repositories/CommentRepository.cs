using Microsoft.EntityFrameworkCore;
using ProductManager.Repositories.Entities;
using ProductManager.Repositories.Interfaces;
using ProductManager.Repositories.Models;

namespace ProductManager.Repositories
{
    public class CommentRepository : IDataRepository<CommentModel>
    {
        readonly ProductManagerDbContext _context;
        public CommentRepository(ProductManagerDbContext context)
        {
            _context = context;
        }

        public async Task Add(CommentModel entity)
        {
            var newEntity = new Comment
            {
                CommentContent= entity.CommentContent,
                Email= entity.Email,
                ProductID= entity.ProductID
            };
            await _context.Comments.AddAsync(newEntity);
            await _context.SaveChangesAsync();
        }

        public async Task AddBulk(IEnumerable<CommentModel> entities)
        {
            var newEntities = new List<Comment>();
            foreach (var entity in entities)
            {
                newEntities.Add(new Comment
                {
                    CommentContent = entity.CommentContent,
                    Email = entity.Email,
                    ProductID = entity.ProductID
                });
            }
            await _context.Comments.AddRangeAsync(newEntities);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(CommentModel entity)
        {
            var entityToDelete = await _context.Comments.FirstOrDefaultAsync(e => e.CommentID == entity.CommentID);

            if (entityToDelete == null)
               throw new Exception("The Comment cannot be found");

            _context.Comments.Remove(entityToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<CommentModel> Get(int id)
        {
            return await _context.Comments.Include(x => x.Product)
                .Select(x => new CommentModel
                {
                    CommentID = x.CommentID,
                    CommentContent = x.CommentContent,
                    Email = x.Email,
                    DateOfComment = x.DateOfComment,
                    ProductID = x.ProductID
                }).FirstOrDefaultAsync(e => e.CommentID == id);
        }

        public async Task<IEnumerable<CommentModel>> GetAll()
        {
            return await _context.Comments.Include(x => x.Product)
                .Select(x => new CommentModel
                {
                    CommentID = x.CommentID,
                    CommentContent = x.CommentContent,
                    Email = x.Email,
                    DateOfComment = x.DateOfComment,
                    ProductID = x.ProductID
                }).ToListAsync();
        }

        public async Task Update(int id, CommentModel newEntity)
        {
            if (newEntity == null)
                throw new Exception("The Comment cannot be null");

            var oldEntity = await _context.Comments.FirstOrDefaultAsync(e => e.CommentID == id);

            if (oldEntity == null)
                throw new Exception("The Comment cannot be found");

            oldEntity.CommentContent = newEntity.CommentContent;
            oldEntity.Email = newEntity.Email;
            oldEntity.DateOfComment = newEntity.DateOfComment;

            await  _context.SaveChangesAsync();
        }
    }
}
