﻿using CodeFlix.Catalog.Application.UseCases.Category.GetCategory;
using FluentAssertions;
using Xunit;

namespace CodeFlix.Catalog.UnitTest.Application.GetCategory
{
    [Collection(nameof(GetCategoryTestFixture))]
    public class GetCategoryInputValidatorTest
    {
        private readonly GetCategoryTestFixture _fixture;

        public GetCategoryInputValidatorTest(GetCategoryTestFixture fixture) 
            => _fixture = fixture;

        [Fact(DisplayName = "ValidationOK")]
        [Trait("Application", "GetCategoryInputValidation - UseCases")]
        public void ValidationOK()
        {
            var validInput = new GetCategoryInput(Guid.NewGuid());
            var validator = new GetCategoryInputValidator();

            var validationResult = validator.Validate(validInput);

            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeTrue();
            validationResult.Errors.Should().HaveCount(0);
        }

        [Fact(DisplayName = "InvalidWhenEmptyGuidId")]
        [Trait("Application", "GetCategoryInputValidation - UseCases")]
        public void InvalidWhenEmptyGuidId()
        {
            var validInput = new GetCategoryInput(Guid.Empty);
            var validator = new GetCategoryInputValidator();

            var validationResult = validator.Validate(validInput);

            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCount(1);
            validationResult.Errors[0].ErrorMessage.Should().Be("'Id' deve ser informado.");
        }
    }
}
