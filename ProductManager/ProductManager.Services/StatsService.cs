using ProductManager.Services.Interfaces;
using ProductManager.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Services
{
    public class StatsService : IStatsService
    {
        private readonly IProductService _productService;
        private readonly ICommentService _commentsService;

        public StatsService(IProductService productsService, ICommentService commentsService)
        {
            _productService = productsService;
            _commentsService = commentsService;
        }

        public async Task<StatsModel> GetStats()
        {
            var stats = new StatsModel();

            var products = await _productService.GetProducts();
            var comments = await _commentsService.GetComments();

            stats.TotalProducts = products != null ? products.Count() : 0;
            stats.TotalComments = comments != null ? comments.Count() : 0;
            stats.ProductsStats = new List<ProductsStatsModel>();

            foreach (var product in products)
            {
                stats.ProductsStats.Add(new ProductsStatsModel
                {
                    Name = product.Name,
                    TotalComments= product.Comments != null ? product.Comments.Count() : 0
                });
            }

            return stats;
        }
    }
}
