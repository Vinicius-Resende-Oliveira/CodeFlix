using CodeFlix.Catalog.Application.Contracts;
using CodeFlix.Catalog.Domain.Entity;
using CodeFlix.Catalog.Domain.Repository;
using CodeFlix.Catalog.UnitTest.Application.Category.Common;
using CodeFlix.Catalog.UnitTest.Commons;
using Moq;
using Xunit;

namespace CodeFlix.Catalog.UnitTest.Application.Category.DeleteCategory
{
    [CollectionDefinition(nameof(DeleteCategoryTestFixture))]
    public class DeleteCategoryTestFixtureCollection : ICollectionFixture<DeleteCategoryTestFixture>
    { }


    public class DeleteCategoryTestFixture
        : CategoryUseCasesBaseFixture
    { }
}
