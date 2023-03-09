using CodeFlix.Catalog.Application.UseCases.Category.DeleteCategory;
using FluentAssertions;
using Xunit;

namespace CodeFlix.Catalog.UnitTest.Application.DeleteCategory
{
    public class DeleteCategoryInputValidatorTest
    {
        [Fact(DisplayName = "ValidationOK")]
        [Trait("Application", "DeleteCategoryInputValidation - UseCases")]
        public void ValidationOK()
        {
            var validInput = new DeleteCategoryInput(Guid.NewGuid());
            var validator = new DeleteCategoryInputValidator();

            var validationResult = validator.Validate(validInput);

            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeTrue();
            validationResult.Errors.Should().HaveCount(0);
        }

        [Fact(DisplayName = "InvalidWhenEmptyGuidId")]
        [Trait("Application", "DeleteCategoryInputValidation - UseCases")]
        public void InvalidWhenEmptyGuidId()
        {
            var validInput = new DeleteCategoryInput(Guid.Empty);
            var validator = new DeleteCategoryInputValidator();

            var validationResult = validator.Validate(validInput);

            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCount(1);
            validationResult.Errors[0].ErrorMessage.Should().Be("'Id' deve ser informado.");
        }
    }
}
