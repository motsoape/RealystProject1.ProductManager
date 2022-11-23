using Microsoft.EntityFrameworkCore;
using ProductManager.Repositories.Entities;
using ProductManager.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Repositories
{
    public class CommentRepository : IDataRepository<Comment>
    {
        readonly ProductManagerDbContext _context;
        public CommentRepository(ProductManagerDbContext context)
        {
            _context = context;
        }

        public async void Add(Comment entity)
        {
            await _context.Comments.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async void AddBulk(IEnumerable<Comment> entities)
        {
            await _context.Comments.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async void Delete(Comment entity)
        {
            _context.Comments.Remove(entity);
            await  _context.SaveChangesAsync();
        }

        public async Task<Comment> Get(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(e => e.CommentID == id);
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            return await _context.Comments.ToListAsync();
        }

        public async void Update(Comment oldEntity, Comment newEntity)
        {
            if (oldEntity != null)
                throw new Exception("The product cannot be updated");

            oldEntity.CommentContent = newEntity.CommentContent;
            oldEntity.Email = newEntity.Email;
            oldEntity.DateOfComment = newEntity.DateOfComment;

            await  _context.SaveChangesAsync();
        }
    }
}
