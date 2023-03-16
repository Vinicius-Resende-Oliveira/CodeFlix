using CodeFlix.Catalog.Application.Exceptions;
using CodeFlix.Catalog.Application.UseCases.Category.UpdateCategory;
using Entity = CodeFlix.Catalog.Domain.Entity;
using CodeFlix.Catalog.Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;
using UseCases = CodeFlix.Catalog.Application.UseCases.Category.UpdateCategory;

namespace CodeFlix.Catalog.UnitTest.Application.Category.UpdateCategory
{
    [Collection(nameof(UpdateCategoryTestFixture))]
    public class UpdateCategoryTest
    {
        private readonly UpdateCategoryTestFixture _fixture;

        public UpdateCategoryTest(UpdateCategoryTestFixture fixture)
            => _fixture = fixture;

        [Theory(DisplayName = nameof(UpdateCategory))]
        [Trait("Application", "UpdateCategory - Use Cases")]
        [MemberData(
            nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
            parameters: 10,
            MemberType = typeof(UpdateCategoryTestDataGenerator)
        )]
        public async Task UpdateCategory(
            UpdateCategoryInput input,
            Entity.Category exampleCategory
        )
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWork();

            repositoryMock.Setup(x =>
                x.Get(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()
                )).ReturnsAsync(exampleCategory);

            var useCase = new UseCases.UpdateCategory(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );

            var output = await useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(
                r => r.Get(
                    exampleCategory.Id,
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );

            repositoryMock.Verify(
                r => r.Update(
                    It.IsAny<Entity.Category>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );

            unitOfWorkMock.Verify(
                uow => uow.Commit(It.IsAny<CancellationToken>()),
                Times.Once
            );

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be((bool)input.IsActive!);
            output.Id.Should().Be(input.Id);
            output.CreateAt.Should().NotBeSameDateAs(default);
        }

        [Theory(DisplayName = nameof(UpdateCategoryWithoutProvidingIsActive))]
        [Trait("Application", "UpdateCategory - Use Cases")]
        [MemberData(
            nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
            parameters: 10,
            MemberType = typeof(UpdateCategoryTestDataGenerator)
        )]
        public async Task UpdateCategoryWithoutProvidingIsActive(
            UpdateCategoryInput exampleInput,
            Entity.Category exampleCategory
        )
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWork();
            var input = new UpdateCategoryInput(
                  exampleInput.Id,
                  exampleInput.Name,
                  exampleInput.Description);

            repositoryMock.Setup(x =>
                x.Get(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()
                )).ReturnsAsync(exampleCategory);

            var useCase = new UseCases.UpdateCategory(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );

            var output = await useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(
                r => r.Get(
                    exampleCategory.Id,
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );

            repositoryMock.Verify(
                r => r.Update(
                    It.IsAny<Entity.Category>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );

            unitOfWorkMock.Verify(
                uow => uow.Commit(It.IsAny<CancellationToken>()),
                Times.Once
            );

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be(exampleCategory.IsActive);
            output.Id.Should().Be(input.Id);
            output.CreateAt.Should().NotBeSameDateAs(default);
        }

        [Theory(DisplayName = nameof(UpdateCategoryWithoutProvidingDescription))]
        [Trait("Application", "UpdateCategory - Use Cases")]
        [MemberData(
            nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
            parameters: 10,
            MemberType = typeof(UpdateCategoryTestDataGenerator)
        )]
        public async Task UpdateCategoryWithoutProvidingDescription(
            UpdateCategoryInput exampleInput,
            Entity.Category exampleCategory
        )
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWork();
            var input = new UpdateCategoryInput(
                  exampleInput.Id,
                  exampleInput.Name,
                  null,
                  exampleInput.IsActive);

            repositoryMock.Setup(x =>
                x.Get(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()
                )).ReturnsAsync(exampleCategory);

            var useCase = new UseCases.UpdateCategory(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );

            var output = await useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(
                r => r.Get(
                    exampleCategory.Id,
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );

            repositoryMock.Verify(
                r => r.Update(
                    It.IsAny<Entity.Category>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );

            unitOfWorkMock.Verify(
                uow => uow.Commit(It.IsAny<CancellationToken>()),
                Times.Once
            );

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(exampleCategory.Description);
            output.IsActive.Should().Be((bool)input.IsActive!);
            output.Id.Should().Be(input.Id);
            output.CreateAt.Should().NotBeSameDateAs(default);
        }

        [Theory(DisplayName = nameof(UpdateCategoryWithOnlyName))]
        [Trait("Application", "UpdateCategory - Use Cases")]
        [MemberData(
            nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
            parameters: 10,
            MemberType = typeof(UpdateCategoryTestDataGenerator)
        )]
        public async Task UpdateCategoryWithOnlyName(
            UpdateCategoryInput exampleInput,
            Entity.Category exampleCategory
        )
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWork();
            var input = new UpdateCategoryInput(
                  exampleInput.Id,
                  exampleInput.Name);

            repositoryMock.Setup(x =>
                x.Get(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()
                )).ReturnsAsync(exampleCategory);

            var useCase = new UseCases.UpdateCategory(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );

            var output = await useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(
                r => r.Get(
                    exampleCategory.Id,
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );

            repositoryMock.Verify(
                r => r.Update(
                    It.IsAny<Entity.Category>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );

            unitOfWorkMock.Verify(
                uow => uow.Commit(It.IsAny<CancellationToken>()),
                Times.Once
            );

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(exampleCategory.Description);
            output.IsActive.Should().Be(exampleCategory.IsActive);
            output.Id.Should().Be(input.Id);
            output.CreateAt.Should().NotBeSameDateAs(default);
        }

        [Fact(DisplayName = "NotFoundExceptionWhenCategoryDoesntExist")]
        [Trait("Application", "UpdateCategory - Use Cases")]
        public async Task NotFoundExceptionWhenCategoryDoesntExist()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWork();
            var exampleGuidId = Guid.NewGuid();

            repositoryMock.Setup(x =>
                x.Get(
                    exampleGuidId,
                    It.IsAny<CancellationToken>()
                )).ThrowsAsync(
                    new NotFoundException($"Category '{exampleGuidId}' not found")
                );

            var useCase = new UseCases.UpdateCategory(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );

            var input = _fixture.GetInput(exampleGuidId);
            var task = async ()
                => await useCase.Handle(input, CancellationToken.None);

            await task.Should().ThrowAsync<NotFoundException>();

            repositoryMock.Verify(x => x.Get(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }

        [Theory(DisplayName = nameof(ThrowWhenCantUpdateAggregate))]
        [Trait("Application", "CreateCategory - Use Cases")]
        [MemberData(
            nameof(UpdateCategoryTestDataGenerator.GetInvalidInputs),
            parameters: 18,
            MemberType = typeof(UpdateCategoryTestDataGenerator)
        )]
        public async void ThrowWhenCantUpdateAggregate(
            UpdateCategoryInput input,
            Entity.Category exampleCategory,
            string exceptionMessage
        )
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWork();

            repositoryMock.Setup(x =>
                x.Get(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()
                )).ReturnsAsync(exampleCategory);

            var useCase = new UseCases.UpdateCategory(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );

            Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

            await task.Should()
                .ThrowAsync<EntityValidationException>()
                .WithMessage(exceptionMessage);
        }
    }
}
