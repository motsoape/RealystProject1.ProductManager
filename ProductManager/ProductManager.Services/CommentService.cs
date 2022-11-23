using ProductManager.Repositories;
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
    public class CommentService : ICommentService
    {
        private readonly IDataRepository<Comment> _commentRepository;

        public CommentService(IDataRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Comment> GetComment(int id)
        {
            return await _commentRepository.Get(id);
        }

        public async Task<IEnumerable<Comment>> GetComments()
        {
            return await _commentRepository.GetAll();
        }

        public void AddComment(Comment comment)
        {
            if (comment == null)
                throw new ArgumentException("Comment cannot be null");

            _commentRepository.Add(comment);
        }

        public void UpdateComment(Comment comment)
        {
            if (comment == null)
                throw new ArgumentException("Comment cannot be null");

            _commentRepository.Update(comment);
        }

        public void DeleteComment(Comment comment)
        {
            if (comment == null)
                throw new ArgumentException("Comment cannot be null");

            _commentRepository.Delete(comment);
        }
    }
}
