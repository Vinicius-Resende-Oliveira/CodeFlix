using CodeFlix.Catalog.Application.Contracts;
using CodeFlix.Catalog.Application.UseCases.Category.UpdateCategory;
using CodeFlix.Catalog.Domain.Entity;
using CodeFlix.Catalog.Domain.Repository;
using CodeFlix.Catalog.UnitTest.Commons;
using Moq;
using Xunit;

namespace CodeFlix.Catalog.UnitTest.Application.UpdateCategory
{
    [CollectionDefinition(nameof(UpdateCategoryTestFixture))]
    public class UpdateCategoryTestFixtureCollection : ICollectionFixture<UpdateCategoryTestFixture>
    { }

    public class UpdateCategoryTestFixture : BaseFixture
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

        public UpdateCategoryInput GetInvalidInputShortName(Guid? id = null)
        {
            var invalidInputShortName = GetInput(id);
            invalidInputShortName.Name = invalidInputShortName.Name.Substring(0, 2);
            return invalidInputShortName;
        }

        public UpdateCategoryInput GetInvalidInputTooLongName(Guid? id = null)
        {
            var invalidInputTooLongName = GetInput(id);
            var TooLongNameForCategory = "";
            while (TooLongNameForCategory.Length <= 255)
            {
                TooLongNameForCategory = $"{TooLongNameForCategory} {Faker.Commerce.ProductName()}";
            }
            invalidInputTooLongName.Name = TooLongNameForCategory;
            return invalidInputTooLongName;
        }

        public UpdateCategoryInput GetInvalidInputNameNull(Guid? id = null)
        {
            var invalidInputNameNull = GetInput(id);
            invalidInputNameNull.Name = null!;
            return invalidInputNameNull;
        }

        public UpdateCategoryInput GetInvalidInputNameEmpty(Guid? id = null)
        {
            var invalidInputNameNull = GetInput(id);
            invalidInputNameNull.Name = "";
            return invalidInputNameNull;
        }

        public UpdateCategoryInput GetInvalidInputDescriptionNull(Guid? id = null)
        {
            var invalidInputDescriptionNull = GetInput(id);
            invalidInputDescriptionNull.Description = null!;
            return invalidInputDescriptionNull;
        }

        public UpdateCategoryInput GetInvalidInputTooLongDescription(Guid? id = null)
        {
            var invalidInputTooLongDescription = GetInput(id);
            var TooLongDescriptionForCategory = "";
            while (TooLongDescriptionForCategory.Length <= 10_000)
            {
                TooLongDescriptionForCategory = $"{TooLongDescriptionForCategory} {Faker.Commerce.ProductDescription()}";
            }
            invalidInputTooLongDescription.Description = TooLongDescriptionForCategory;
            return invalidInputTooLongDescription;
        }

        public bool getRandomBoolen()
            => (new Random()).NextDouble() < 0.5;

        public UpdateCategoryInput GetInput(Guid? id = null)
            => new(
                id ?? Guid.NewGuid(),
                GetValidCategoryName(),
                GetValidCategoryDescription(),
                getRandomBoolen()
                );

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
