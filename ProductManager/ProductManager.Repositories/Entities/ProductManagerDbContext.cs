using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Repositories.Entities
{
    public class ProductManagerDbContext : DbContext
    {
        public ProductManagerDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany<Comment>(g => g.Comments)
                .WithOne(s => s.Product)
                .HasForeignKey(s => s.ProductID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>().HasData(
                    new Product
                    {
                        ProductID = 1,
                        Price = 19.99,
                        Name = "Lays",
                        ReleaseDate= DateTime.Now,
                    },
                    new Product
                    {
                        ProductID = 2,
                        Price = 35.99,
                        Name = "Lipton",
                        ReleaseDate = DateTime.Now,
                    }
                );

            modelBuilder.Entity<Comment>().HasData(
                    new Comment
                    {
                        CommentID= 1,
                        CommentContent = "I like it...",
                        Email = "name.surname@test.com",
                        DateOfComment= DateTime.Now,
                        ProductID = 1
                    }
                );
        }
    }
}
