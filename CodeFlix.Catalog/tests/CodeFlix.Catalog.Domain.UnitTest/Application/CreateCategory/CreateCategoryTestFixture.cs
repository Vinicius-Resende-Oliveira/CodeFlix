using CodeFlix.Catalog.Application.Contracts;
using CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;
using CodeFlix.Catalog.Domain.Repository;
using CodeFlix.Catalog.UnitTest.Commons;
using Moq;
using Xunit;

namespace CodeFlix.Catalog.UnitTest.Application.CreateCategory
{
    [CollectionDefinition(nameof(CreateCategoryTestFixture))]
    public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture>
    { }


    public class CreateCategoryTestFixture : BaseFixture
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

        public bool getRandomBoolen()
            => (new Random()).NextDouble() < 0.5;

        public CreateCategoryInput GetInput()
            => new(
                GetValidCategoryName(),
                GetValidCategoryDescription(),
                getRandomBoolen()
                );

        public CreateCategoryInput GetInvalidInputShortName()
        {
            var invalidInputShortName = GetInput();
            invalidInputShortName.Name = invalidInputShortName.Name.Substring(0, 2);
            return invalidInputShortName;
        }

        public CreateCategoryInput GetInvalidInputTooLongName()
        {
            var invalidInputTooLongName = GetInput();
            var TooLongNameForCategory = "";
            while (TooLongNameForCategory.Length <= 255)
            {
                TooLongNameForCategory = $"{TooLongNameForCategory} {Faker.Commerce.ProductName()}";
            }
            invalidInputTooLongName.Name = TooLongNameForCategory;
            return invalidInputTooLongName;
        }

        public CreateCategoryInput GetInvalidInputNameNull()
        {
            var invalidInputNameNull = GetInput();
            invalidInputNameNull.Name = null!;
            return invalidInputNameNull;
        }

        public CreateCategoryInput GetInvalidInputNameEmpty()
        {
            var invalidInputNameNull = GetInput();
            invalidInputNameNull.Name = "";
            return invalidInputNameNull;
        }

        public CreateCategoryInput GetInvalidInputDescriptionNull()
        {
            var invalidInputDescriptionNull = GetInput();
            invalidInputDescriptionNull.Description = null!;
            return invalidInputDescriptionNull;
        }

        public CreateCategoryInput GetInvalidInputTooLongDescription()
        {
            var invalidInputTooLongDescription = GetInput();
            var TooLongDescriptionForCategory = "";
            while (TooLongDescriptionForCategory.Length <= 10_000)
            {
                TooLongDescriptionForCategory = $"{TooLongDescriptionForCategory} {Faker.Commerce.ProductDescription()}";
            }
            invalidInputTooLongDescription.Description = TooLongDescriptionForCategory;
            return invalidInputTooLongDescription;
        }

        public Mock<ICategoryRepository> GetRepositoryMock()
            => new();

        public Mock<IUnitOfWork> GetUnitOfWork()
            => new();
    }
}
