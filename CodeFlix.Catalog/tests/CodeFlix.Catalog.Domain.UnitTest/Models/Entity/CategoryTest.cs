﻿using DomainEntity = CodeFlix.Catalog.Domain.Entity;
using Xunit;
using FluentAssertions;
using CodeFlix.Catalog.Domain.Exceptions;

namespace CodeFlix.Catalog.UnitTest.Models.Entity
{
    [Collection(nameof(CategoryTestFixture))]
    public class CategoryTest
    {
        private readonly CategoryTestFixture _categoryTestFixture;

        public CategoryTest(CategoryTestFixture categoryTestFixture)
            => _categoryTestFixture = categoryTestFixture; 

        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Category Aggregates")]
        public void Instantiate()
        {
            var validCategory = _categoryTestFixture.GetValidCategory();

            var datetimebefore = DateTime.Now;
            var category = new DomainEntity.Category(validCategory.Name, validCategory.Description);
            var datetimeafter = DateTime.Now.AddSeconds(1);

            category.Should().NotBeNull();
            category.Name.Should().Be(validCategory.Name);
            category.Description.Should().Be(validCategory.Description);
            category.Id.Should().NotBeEmpty();
            category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
            (category.CreatedAt >= datetimebefore).Should().BeTrue();
            (category.CreatedAt <= datetimeafter).Should().BeTrue();
            category.IsActive.Should().BeTrue();
        }

        [Theory(DisplayName =nameof(InstantiateWithIsActive))]
        [Trait("Domain", "Category Aggregates")]
        [InlineData(true)]
        [InlineData(false)]
        public void InstantiateWithIsActive(bool isActive)
        {
            var validCategory = _categoryTestFixture.GetValidCategory();

            var datetimebefore = DateTime.Now;
            var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, isActive);
            var datetimeafter = DateTime.Now.AddSeconds(1);

            category.Should().NotBeNull();
            category.Name.Should().Be(validCategory.Name);
            category.Description.Should().Be(validCategory.Description);
            category.Id.Should().NotBeEmpty();
            category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
            (category.CreatedAt >= datetimebefore).Should().BeTrue();
            (category.CreatedAt <= datetimeafter).Should().BeTrue();
            category.IsActive.Should().Be(isActive);
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
        [Trait("Domain", "Category Aggregates")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void InstantiateErrorWhenNameIsEmpty(string? invalidName)
        {
            var validCategory = _categoryTestFixture.GetValidCategory();

            Action action =
                () => new DomainEntity.Category(invalidName!, validCategory.Description);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should not be null or empty");
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
        [Trait("Domain", "Category Aggregates")]
        public void InstantiateErrorWhenDescriptionIsNull()
        {
            var validCategory = _categoryTestFixture.GetValidCategory();

            Action action =
                () => new DomainEntity.Category(validCategory.Name, null!);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Description should not be null");
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThen3Characters))]
        [Trait("Domain", "Category Aggregates")]
        [MemberData(nameof(GetNameWhenNameIsLessThen3Characters), parameters: 10)]
        public void InstantiateErrorWhenNameIsLessThen3Characters(string invalidName)
        {
            var validCategory = _categoryTestFixture.GetValidCategory();

            Action action =
                () => new DomainEntity.Category(invalidName, validCategory.Description);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should be at least 3 characters long");
        }

        public static IEnumerable<object[]> GetNameWhenNameIsLessThen3Characters(int numberOfTests = 6)
        {
            var fixture = new CategoryTestFixture();
            for (int i = 0; i < numberOfTests; i++)
            {
                var idOdd = i % 2 == 1;
                yield return new object[] 
                {
                    fixture.GetValidCategoryName()[..(idOdd ? 1 : 2)] 
                };

            }
        }

        [Fact(DisplayName = (nameof(InstantiateErrorWhenNameIsGreaterThan255Characters)))]
        [Trait("Domain", "Category Aggregates")]
        public void InstantiateErrorWhenNameIsGreaterThan255Characters()
        {
            var validCategory = _categoryTestFixture.GetValidCategory();
            var invalidName = String.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());

            Action action =
                () => new DomainEntity.Category(invalidName, validCategory.Description);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should be less or equal 255 characters long");
        }

        [Fact(DisplayName = (nameof(InstantiateErrorWhenDescriptionIsGreaterThan10_000Characters)))]
        [Trait("Domain", "Category Aggregates")]
        public void InstantiateErrorWhenDescriptionIsGreaterThan10_000Characters()
        {
            var validCategory = _categoryTestFixture.GetValidCategory();
            var invaliDescription = String.Join(null, Enumerable.Range(1, 10_001).Select(_ => "a").ToArray());

            Action action =
                () => new DomainEntity.Category(validCategory.Name, invaliDescription);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Description should be less or equal 10000 characters long");
        }

        [Fact(DisplayName = (nameof(Activate)))]
        [Trait("Domain", "Category Aggregates")]
        public void Activate()
        {
            var validCategory = _categoryTestFixture.GetValidCategory();

            var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, false);
            category.Activate();

            category.IsActive.Should().BeTrue();
        }

        [Fact(DisplayName = (nameof(Deactivate)))]
        [Trait("Domain", "Category Aggregates")]
        public void Deactivate()
        {
            var validCategory = _categoryTestFixture.GetValidCategory();

            var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, true);
            category.Deactivate();

            category.IsActive.Should().BeFalse();
        }

        [Fact(DisplayName = (nameof(Update)))]
        [Trait("Domain", "Category Aggregates")]
        public void Update()
        {
            var category = _categoryTestFixture.GetValidCategory();
            var categoryWithNewValues = _categoryTestFixture.GetValidCategory();

            category.Update(categoryWithNewValues.Name, categoryWithNewValues.Description);

            category.Name.Should().Be(categoryWithNewValues.Name);
            category.Description.Should().Be(categoryWithNewValues.Description);
        }

        [Fact(DisplayName = (nameof(UpdateOnlyName)))]
        [Trait("Domain", "Category Aggregates")]
        public void UpdateOnlyName()
        {
            var category = _categoryTestFixture.GetValidCategory();
            var categoryWithNewValues = _categoryTestFixture.GetValidCategory();

            category.Update(categoryWithNewValues.Name);

            category.Name.Should().Be(categoryWithNewValues.Name);
            category.Description.Should().Be(category.Description);
        }

        [Theory(DisplayName = nameof(UpdateErrorWhenNameIsEmpty))]
        [Trait("Domain", "Category Aggregates")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void UpdateErrorWhenNameIsEmpty(string? invalidName)
        {
            var category = _categoryTestFixture.GetValidCategory();

            Action action =
                () => category.Update(invalidName!);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should not be null or empty");
        }

        [Theory(DisplayName = nameof(UpdateErrorWhenNameIsLessThen3Characters))]
        [Trait("Domain", "Category Aggregates")]
        [InlineData("ax")]
        [InlineData("a")]
        [InlineData("12")]
        public void UpdateErrorWhenNameIsLessThen3Characters(string invalidName)
        {
            var category = _categoryTestFixture.GetValidCategory();

            Action action =
                () => category.Update(invalidName!);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should be at least 3 characters long");
        }

        [Fact(DisplayName = (nameof(UpdateErrorWhenNameIsGreaterThan255Characters)))]
        [Trait("Domain", "Category Aggregates")]
        public void UpdateErrorWhenNameIsGreaterThan255Characters()
        {
            var category = _categoryTestFixture.GetValidCategory();
            var invalidName = String.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());

            Action action =
                () => category.Update(invalidName!);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should be less or equal 255 characters long");
        }

        [Fact(DisplayName = (nameof(UpdateErrorWhenDescriptionIsGreaterThan10_000Characters)))]
        [Trait("Domain", "Category Aggregates")]
        public void UpdateErrorWhenDescriptionIsGreaterThan10_000Characters()
        {
            var category = _categoryTestFixture.GetValidCategory();
            var invaliDescription = _categoryTestFixture.Faker.Commerce.ProductDescription();

            while(invaliDescription.Length <= 10_000)
            {
                invaliDescription = $"{invaliDescription} {_categoryTestFixture.Faker.Commerce.ProductDescription()}";
            }

            Action action =
                () => category.Update("Category New Name", invaliDescription);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Description should be less or equal 10000 characters long");
        }
    }
}
