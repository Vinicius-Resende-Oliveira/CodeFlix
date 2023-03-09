using CodeFlix.Catalog.Application.Contracts;
using CodeFlix.Catalog.Domain.Entity;
using CodeFlix.Catalog.Domain.Repository;
using CodeFlix.Catalog.UnitTest.Commons;
using Moq;
using Xunit;

namespace CodeFlix.Catalog.UnitTest.Application.DeleteCategory
{
    [CollectionDefinition(nameof(DeleteCategoryTestFixture))]
    public class DeleteCategoryTestFixtureCollection : ICollectionFixture<DeleteCategoryTestFixture>
    { }


    public class DeleteCategoryTestFixture : BaseFixture
    {
        public string GetValidCategoryName()
        {
            var categoryName = "";
            while (categoryName.Length < 3)
            {
                categoryName = Faker.Commerce.Categories(1)[0];
            }

            if (categoryName.Length > 255)
            {
                categoryName = categoryName[..255];
            }
            return categoryName;
        }

        public string GetValidCategoryDescription()
        {
            var categoryDescription = Faker.Commerce.ProductDescription();
            if (categoryDescription.Length > 10_000)
            {
                categoryDescription = categoryDescription[..10_000];
            }
            return categoryDescription;
        }

        public Category GetValidCategory()
            => new Category(
                GetValidCategoryName(),
                GetValidCategoryDescription()
                );

        public Mock<ICategoryRepository> GetRepositoryMock()
            => new();

        public Mock<IUnitOfWork> GetUnitOfWork()
            => new();
    }
}
