using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Repositories.Entities
{
    public class CommentContext : DbContext
    {
        public CommentContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Comment> Comments { get; set; }

    }
}
