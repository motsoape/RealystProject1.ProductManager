
using Moq;
using ProductManager.Repositories.Interfaces;
using ProductManager.Repositories.Models;
using ProductManager.Services;

namespace ProductManager.UnitTests
{
    public class CommentServiceTest
    {
        private readonly Mock<IDataRepository<CommentModel>> mockCommentRepository = new Mock<IDataRepository<CommentModel>>();

        [Fact]
        public void GetComment_SouldReturn()
        {
            //Arrange
            mockCommentRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(new CommentModel());
            var service = new CommentService(mockCommentRepository.Object);

            //Act
            var result = service.GetComment(1);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetComments_SouldReturnList()
        {
            //Arrange
            mockCommentRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<CommentModel> { new CommentModel() });
            var service = new CommentService(mockCommentRepository.Object);

            //Act
            var result = await service.GetComments();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Any());
        }

        [Fact]
        public async void AddComment_SouldReturn()
        {
            //Arrange
            mockCommentRepository.Setup(x => x.Add(It.IsAny<CommentModel>())).Verifiable();
            var service = new CommentService(mockCommentRepository.Object);

            //Act
            await service.AddComment(new CommentModel());

            //Assert
            mockCommentRepository.Verify(x => x.Add(It.IsAny<CommentModel>()), Times.Once);
        }

        [Fact]
        public async void AddComment_ThrowsException()
        {
            //Arrange
            var service = new CommentService(mockCommentRepository.Object);


            //Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await service.AddComment(null));
            Assert.Equal("Comment cannot be null", ex.Message);
        }

        /*
         * More unit tests can be added for good coverage
         */
    }
}
