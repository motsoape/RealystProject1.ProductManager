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

        public void Add(Comment entity)
        {
            _commentContext.Comments.Add(entity);
            _commentContext.SaveChanges();
        }

        public void Delete(Comment entity)
        {
            _commentContext.Comments.Remove(entity);
            _commentContext.SaveChanges();
        }

        public Comment Get(int id)
        {
            return _commentContext.Comments.FirstOrDefault(e => e.CommentID == id);
        }

        public IEnumerable<Comment> GetAll()
        {
            return _commentContext.Comments.ToList();
        }

        public void Update(Comment entity)
        {
            var _entityToUpdate = Get(entity.CommentID);
            if (_entityToUpdate != null)
                throw new Exception("The product cannot be updated");

            _entityToUpdate.CommentContent = entity.CommentContent;
            _entityToUpdate.Email = entity.Email;
            _entityToUpdate.DateOfComment = entity.DateOfComment;

            _commentContext.SaveChanges();
        }
    }
}
