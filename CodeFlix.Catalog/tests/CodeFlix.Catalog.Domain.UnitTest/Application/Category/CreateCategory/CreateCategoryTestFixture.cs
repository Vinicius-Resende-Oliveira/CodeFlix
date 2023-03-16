using CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;
using CodeFlix.Catalog.UnitTest.Application.Category.Common;
using Xunit;

namespace CodeFlix.Catalog.UnitTest.Application.Category.CreateCategory
{
    [CollectionDefinition(nameof(CreateCategoryTestFixture))]
    public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture>
    { }


    public class CreateCategoryTestFixture
        : CategoryUseCasesBaseFixture
    {
        public CreateCategoryInput GetInput()
            => new(
                GetValidCategoryName(),
                GetValidCategoryDescription(),
                GetRandomBoolen()
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
    }
}
