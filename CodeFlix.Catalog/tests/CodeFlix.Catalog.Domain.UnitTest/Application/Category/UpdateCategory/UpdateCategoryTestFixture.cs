using CodeFlix.Catalog.Application.UseCases.Category.UpdateCategory;
using CodeFlix.Catalog.UnitTest.Application.Category.Common;
using Xunit;

namespace CodeFlix.Catalog.UnitTest.Application.Category.UpdateCategory
{
    [CollectionDefinition(nameof(UpdateCategoryTestFixture))]
    public class UpdateCategoryTestFixtureCollection : ICollectionFixture<UpdateCategoryTestFixture>
    { }

    public class UpdateCategoryTestFixture
        : CategoryUseCasesBaseFixture
    {
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

        public UpdateCategoryInput GetInput(Guid? id = null)
            => new(
                id ?? Guid.NewGuid(),
                GetValidCategoryName(),
                GetValidCategoryDescription(),
                GetRandomBoolen()
            );
    }
}
