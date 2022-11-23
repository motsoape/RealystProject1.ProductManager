using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Repositories.Entities
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string CommentContent { get; set; }
        public string Email { get; set; }
        public DateTime DateOfComment { get; set; }

    }
}
