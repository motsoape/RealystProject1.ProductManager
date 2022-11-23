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
        readonly CommentContext _commentContext;
        public CommentRepository(CommentContext context)
        {
            _commentContext = context;
        }

        public async void Add(Comment entity)
        {
            await _commentContext.Comments.AddAsync(entity);
            await _commentContext.SaveChangesAsync();
        }

        public async void AddBulk(IEnumerable<Comment> entities)
        {
            await _commentContext.Comments.AddRangeAsync(entities);
            await _commentContext.SaveChangesAsync();
        }

        public async void Delete(Comment entity)
        {
            _commentContext.Comments.Remove(entity);
            await  _commentContext.SaveChangesAsync();
        }

        public async Task<Comment> Get(int id)
        {
            return await _commentContext.Comments.FirstOrDefaultAsync(e => e.CommentID == id);
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            return await _commentContext.Comments.ToListAsync();
        }

        public async void Update(Comment entity)
        {
            var _entityToUpdate = await Get(entity.CommentID);
            if (_entityToUpdate != null)
                throw new Exception("The product cannot be updated");

            _entityToUpdate.CommentContent = entity.CommentContent;
            _entityToUpdate.Email = entity.Email;
            _entityToUpdate.DateOfComment = entity.DateOfComment;

            await  _commentContext.SaveChangesAsync();
        }
    }
}
