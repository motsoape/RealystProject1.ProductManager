
using Moq;
using ProductManager.Repositories.Models;
using ProductManager.Services;
using ProductManager.Services.Interfaces;

namespace ProductManager.UnitTests
{
    public class StatsServiceTest
    {
        private readonly Mock<IProductService> mockProductService = new Mock<IProductService>();
        private readonly Mock<ICommentService> mockCommentService = new Mock<ICommentService>();

        [Fact]
        public async void GetStats_Succeed()
        {
            //Arrange
            var products = new List<ProductModel>
            {
                new ProductModel(),
                new ProductModel()
            };
            var comments = new List<CommentModel>
            {
                new CommentModel()
            };

            mockProductService.Setup(x => x.GetProducts()).ReturnsAsync(products);
            mockCommentService.Setup(x => x.GetComments()).ReturnsAsync(comments);
            var service = new StatsService(mockProductService.Object, mockCommentService.Object);

            //Act
            var results = await service.GetStats();

            //Assert
            Assert.NotNull(results);
            Assert.Equal(2, results.TotalProducts);
            Assert.Equal(1, results.TotalComments);
        }
    }
}
