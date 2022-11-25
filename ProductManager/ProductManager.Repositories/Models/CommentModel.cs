

using System.ComponentModel.DataAnnotations;

namespace ProductManager.Repositories.Models
{
    public class CommentModel
    {
        public int CommentID { get; set; }
        public string? CommentContent { get; set; }
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public DateTime DateOfComment { get; set; }
        [Required]
        public int ProductID { get; set; }
    }
}
