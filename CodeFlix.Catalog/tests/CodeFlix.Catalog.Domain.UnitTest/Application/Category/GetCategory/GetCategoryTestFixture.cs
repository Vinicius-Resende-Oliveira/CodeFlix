using CodeFlix.Catalog.UnitTest.Application.Category.Common;
using Xunit;

namespace CodeFlix.Catalog.UnitTest.Application.Category.GetCategory
{

    [CollectionDefinition(nameof(GetCategoryTestFixture))]
    public class GetCategoryTestFixtureCollection :
        ICollectionFixture<GetCategoryTestFixture>
    { }

    public class GetCategoryTestFixture
        : CategoryUseCasesBaseFixture
    { }
}
