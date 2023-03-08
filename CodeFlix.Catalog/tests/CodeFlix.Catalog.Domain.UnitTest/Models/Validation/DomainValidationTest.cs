using Bogus;
using Xunit;
using CodeFlix.Catalog.Domain.Validation;
using FluentAssertions;
using CodeFlix.Catalog.Domain.Exceptions;

namespace CodeFlix.Catalog.UnitTest.Models.Validation
{
    public class DomainValidationTest
    {
        private Faker Faker { get; set; } = new Faker();

        [Fact(DisplayName = nameof(NotNullOk))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOk()
        {
            var value = Faker.Commerce.ProductName();
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action action = 
                () => DomainValidation.NotNull(value, fieldName);

            action.Should()
                .NotThrow();
        }

        [Fact(DisplayName = nameof(NotNullThrowWhenNull))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullThrowWhenNull()
        {
            string? value = null;
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action action =
                () => DomainValidation.NotNull(value, fieldName);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage($"{fieldName} should not be null");
        }

        [Fact(DisplayName = nameof(NotNullOrEmptyOk))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOrEmptyOk()
        {
            var value = Faker.Commerce.ProductName();
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action action =
                () => DomainValidation.NotNullOrEmpty(value, fieldName);

            action.Should()
                .NotThrow();
        }

        [Theory(DisplayName = nameof(NotNullThrowWhenNull))]
        [Trait("Domain", "DomainValidation - Validation")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void NotNullOrEmptyThrowWhenEmpty(string invalidTarget)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
            Action action =
                () => DomainValidation.NotNullOrEmpty(invalidTarget, fieldName);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage($"{fieldName} should not be null or empty");
        }

        [Theory(DisplayName = nameof(MinLengthThrowWhenLess))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesSmallerThanTheMin), parameters: 10)]
        public void MinLengthThrowWhenLess(string target, int minLength)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
            Action action =
                () => DomainValidation.MinLength(target, minLength, fieldName);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage($"{fieldName} should be at least {minLength} characters long");
        }

        public static IEnumerable<object[]> GetValuesSmallerThanTheMin(int numberofTests)
        {
            yield return new object[] { "123456", 7 };
            var faker = new Faker();
            for (int i = 0; i < numberofTests; i++)
            {
                var example = faker.Commerce.ProductName();
                var minLength = example.Length + (new Random()).Next(1, 20);
                yield return new object[] { example, minLength };
            }
        }

        [Theory(DisplayName = nameof(MinLengthOk))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesGreaterThanTheMin), parameters: 10)]
        public void MinLengthOk(string target, int minLength)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
            Action action =
                () => DomainValidation.MinLength(target, minLength, fieldName);

            action.Should()
                .NotThrow();
        }

        public static IEnumerable<object[]> GetValuesGreaterThanTheMin(int numberofTests)
        {
            yield return new object[] { "123456", 3 };
            var faker = new Faker();
            for (int i = 0; i < numberofTests; i++)
            {
                var example = faker.Commerce.ProductName();
                var minLength = example.Length - (new Random()).Next(1, example.Length);
                yield return new object[] { example, minLength };
            }
        }

        [Theory(DisplayName = nameof(MaxLengthThrowWhenGreater))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesGreaterThanTheMax), parameters: 10)]
        public void MaxLengthThrowWhenGreater(string target, int maxLength)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
            Action action =
                () => DomainValidation.MaxLength(target, maxLength, fieldName);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage($"{fieldName} should be less or equal {maxLength} characters long");
        }

        public static IEnumerable<object[]> GetValuesGreaterThanTheMax(int numberofTests)
        {
            yield return new object[] { "123456", 5 };
            var faker = new Faker();
            for (int i = 0; i < numberofTests; i++)
            {
                var example = faker.Commerce.ProductDescription();
                var maxLength = example.Length - (new Random()).Next(1, example.Length);
                yield return new object[] { example, maxLength };
            }
        }

        [Theory(DisplayName = nameof(MaxLengthOk))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesSmallerThanTheMax), parameters: 10)]
        public void MaxLengthOk(string target, int maxLength)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
            Action action =
                () => DomainValidation.MaxLength(target, maxLength, fieldName);

            action.Should()
                .NotThrow();
        }

        public static IEnumerable<object[]> GetValuesSmallerThanTheMax(int numberofTests)
        {
            yield return new object[] { "123456", 7 };
            var faker = new Faker();
            for (int i = 0; i < numberofTests; i++)
            {
                var example = faker.Commerce.ProductName();
                var maxLength = example.Length + (new Random()).Next(1, example.Length);
                yield return new object[] { example, maxLength };
            }
        }
    }
}
