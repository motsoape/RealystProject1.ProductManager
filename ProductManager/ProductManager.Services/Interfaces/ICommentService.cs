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
    public interface ICommentService
    {
        Task AddComment(CommentModel comment);
        Task DeleteComment(int commentId);
        Task<CommentModel> GetComment(int id);
        Task<IEnumerable<CommentModel>> GetComments();
        Task UpdateComment(int commentId, CommentModel newComment);
    }
}
