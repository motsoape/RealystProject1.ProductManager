﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Repositories.Entities
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
    }
}
