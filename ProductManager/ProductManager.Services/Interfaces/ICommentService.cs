using ProductManager.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Services.Interfaces
{
    public interface ICommentService
    {
        void AddComment(Comment comment);
        void DeleteComment(Comment comment);
        Task<Comment> GetComment(int id);
        Task<IEnumerable<Comment>> GetComments();
        void UpdateComment(Comment comment);
    }
}
