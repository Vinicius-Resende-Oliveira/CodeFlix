using CodeFlix.Catalog.Application.Exceptions;
using Entity = CodeFlix.Catalog.Domain.Entity;
using FluentAssertions;
using Moq;
using Xunit;
using UseCases = CodeFlix.Catalog.Application.UseCases.Category.DeleteCategory;

namespace CodeFlix.Catalog.UnitTest.Application.Category.DeleteCategory
{
    [Collection(nameof(DeleteCategoryTestFixture))]
    public class DeleteCategoryTest
    {
        private readonly DeleteCategoryTestFixture _fixture;

        public DeleteCategoryTest(DeleteCategoryTestFixture fixture)
            => _fixture = fixture;


        [Fact(DisplayName = "DeleteCategory")]
        [Trait("Application", "DeleteCategory - Use Cases")]
        public async Task DeleteCategory()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWork();
            var exampleCategory = _fixture.GetExampleCategory();

            repositoryMock.Setup(x => x.Get(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(exampleCategory);

            repositoryMock.Setup(x => x.Delete(
                It.IsAny<Entity.Category>(),
                It.IsAny<CancellationToken>()
            ));

            var input = new UseCases.DeleteCategoryInput(exampleCategory.Id);
            var useCase = new UseCases.DeleteCategory(repositoryMock.Object, unitOfWorkMock.Object);

            await useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(x => x.Get(
                exampleCategory.Id,
                CancellationToken.None
            ), Times.Once);
            repositoryMock.Verify(x => x.Delete(
                exampleCategory,
                CancellationToken.None
            ), Times.Once);
            unitOfWorkMock.Verify(x => x.Commit(
                CancellationToken.None
            ), Times.Once);
        }

        [Fact(DisplayName = "NotFoundExceptionWhenCategoryDoesntExist")]
        [Trait("Application", "DeleteCategory - Use Cases")]
        public async Task NotFoundExceptionWhenCategoryDoesntExist()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWork();
            var exampleGuid = Guid.NewGuid();

            repositoryMock.Setup(x =>
                x.Get(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()
                )).ThrowsAsync(
                    new NotFoundException($"Category '{exampleGuid}' not Found")
                );

            var input = new UseCases.DeleteCategoryInput(exampleGuid);
            var useCase = new UseCases.DeleteCategory(repositoryMock.Object, unitOfWorkMock.Object);
            var task = async ()
                => await useCase.Handle(input, CancellationToken.None);

            await task.Should().ThrowAsync<NotFoundException>();

            repositoryMock.Verify(x => x.Get(
                exampleGuid,
                CancellationToken.None
            ), Times.Once);
            repositoryMock.Verify(x => x.Delete(
                It.IsAny<Entity.Category>(),
                CancellationToken.None
            ), Times.Never);
            unitOfWorkMock.Verify(x => x.Commit(
                CancellationToken.None
            ), Times.Never);

        }
    }
}
