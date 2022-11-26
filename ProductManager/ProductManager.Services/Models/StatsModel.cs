using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Services.Models
{
    public class StatsModel
    {
        public int TotalProducts { get; set; }
        public int TotalComments { get; set; }
        public List<ProductsStatsModel> ProductsStats { get; set;  }
    }
}
