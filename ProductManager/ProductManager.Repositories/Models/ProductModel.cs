

using System.ComponentModel.DataAnnotations;

namespace ProductManager.Repositories.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        public IEnumerable<CommentModel>? Comments { get; set; }
    }
}
