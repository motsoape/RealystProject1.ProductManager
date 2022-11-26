using Moq;
using ProductManager.Repositories.Interfaces;
using ProductManager.Repositories.Models;
using ProductManager.Services;
using System;

namespace ProductManager.UnitTests
{
    public class ProductServiceTest
    {
        private readonly Mock<IDataRepository<ProductModel>> mockProductRepository = new Mock<IDataRepository<ProductModel>>();

        [Fact]
        public void GetProduct_SouldReturn()
        {
            //Arrange
            mockProductRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(new ProductModel());
            var service = new ProductService(mockProductRepository.Object);

            //Act
            var result = service.GetProduct(1);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetProducts_SouldReturnList()
        {
            //Arrange
            mockProductRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<ProductModel> { new ProductModel() });
            var service = new ProductService(mockProductRepository.Object);

            //Act
            var result = await service.GetProducts();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Any());
        }

        [Fact]
        public async void AddProduct_SouldReturn()
        {
            //Arrange
            mockProductRepository.Setup(x => x.Add(It.IsAny<ProductModel>())).Verifiable();
            var service = new ProductService(mockProductRepository.Object);

            //Act
            await service.AddProduct(new ProductModel());

            //Assert
            mockProductRepository.Verify(x => x.Add(It.IsAny<ProductModel>()), Times.Once);
        }

        [Fact]
        public async void AddProduct_ThrowsException()
        {
            //Arrange
            var service = new ProductService(mockProductRepository.Object);


            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await service.AddProduct(null));
            Assert.Equal("Product cannot be null", ex.Message);
        }

        /*
         * More unit tests can be added for good coverage
         */
    }
}