using CodeFlix.Catalog.Application.UseCases.Category.UpdateCategory;
using FluentAssertions;
using Xunit;

namespace CodeFlix.Catalog.UnitTest.Application.UpdateCategory
{
    [Collection(nameof(UpdateCategoryTestFixture))]
    public class UpdateCategoryInputValidatorTest
    {
        private readonly UpdateCategoryTestFixture _fixture;

        public UpdateCategoryInputValidatorTest(UpdateCategoryTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = "ValidationOK")]
        [Trait("Application", "UpdateCategoryInputValidator - UseCases")]
        public void ValidationOK()
        {
            var validInput = _fixture.GetInput();
            var validator = new UpdateCategoryInputValidator();

            var validationResult = validator.Validate(validInput);

            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeTrue();
            validationResult.Errors.Should().HaveCount(0);
        }

        [Fact(DisplayName = nameof(DontValidWhenEmptyGuid))]
        [Trait("Application", "UpdateCategoryInputValidator - UseCases")]
        public void DontValidWhenEmptyGuid()
        {
            var input = _fixture.GetInput(Guid.Empty);
            var validator = new UpdateCategoryInputValidator();

            var validateResult = validator.Validate(input);

            validateResult.Should()
                .NotBeNull();
            validateResult.IsValid
                .Should()
                .BeFalse();
            validateResult.Errors
                .Should()
                .HaveCount(1);
            validateResult.Errors[0]
                .ErrorMessage
                .Should()
                .Be("'Id' deve ser informado.");
        }
    }
}
