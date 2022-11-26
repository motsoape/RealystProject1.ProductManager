using ProductManager.Repositories;
using ProductManager.Repositories.Entities;
using ProductManager.Repositories.Interfaces;
using ProductManager.Repositories.Models;
using ProductManager.Services.Interfaces;
using ProductManager.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Services
{
    public class CommentService : ICommentService
    {
        private readonly IDataRepository<CommentModel> _commentRepository;

        public CommentService(IDataRepository<CommentModel> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<CommentModel> GetComment(int id)
        {
            return await _commentRepository.Get(id);
        }

        public async Task<IEnumerable<CommentModel>> GetComments()
        {
            return await _commentRepository.GetAll();
        }

        public async Task AddComment(CommentModel comment)
        {
            if (comment == null)
                throw new ArgumentException("Comment cannot be null");

            await _commentRepository.Add(comment);
        }

        public async Task UpdateComment(int commentId, CommentModel comment)
        {
            if (comment == null)
                throw new ArgumentException("New Comment cannot be null");

            await _commentRepository.Update(commentId, comment);
        }

        public async Task DeleteComment(int commentId)
        {
            var comment = await GetComment(commentId);
            if (comment == null)
                throw new Exception("Comment not found");

            await _commentRepository.Delete(comment);
        }
    }
}
