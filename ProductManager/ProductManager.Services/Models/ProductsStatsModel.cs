using ProductManager.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Services.Models
{
    public class ProductsStatsModel 
    {
        public string Name { get; set; }
        public int TotalComments { get; set; }
    }
}
