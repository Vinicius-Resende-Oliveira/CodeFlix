using CodeFlix.Catalog.Domain.Entity;
using Moq;
using Xunit;

namespace CodeFlix.Catalog.UnitTest.Application.ListCategories
{
    [Collection(nameof(ListCategoriesTestFixture))]
    public class ListCategoriesTest
    {
        private readonly ListCategoriesTestFixture _fixture;

        public ListCategoriesTest(ListCategoriesTestFixture fixture)
            => _fixture = fixture;
        
        [Fact(DisplayName = nameof(List))]
        [Trait("Application", "ListCategories - UseCases")]
        public async Task List()
        {
            var categoriesExampleList = _fixture.GetValidCategoryList();
            var repositoryMock = _fixture.GetRepositoryMock();
            var input = new ListCategoriesInput(
                page: 2,
                perPage: 15,
                search: "search-example",
                sort: "name",
                dir: SearchOrder.Asc
            );

            var outputRepositorySearch = new SearchOutput<Category>(
                currentPage: input.Page,
                perPage: input.PerPage,
                items: (IReadOnlyList<Category>)categoriesExampleList,
                total: 70
            );

            repositoryMock.Setup(x => x.Search(
                It.Is<SearchInput>(
                    searchInput.Page == input.Page
                    && searchInput.PerPage = input.PerPage
                    && searchInput.Search == input.Search
                    && searchInput.OrderBy == input.Sort
                    && searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            )).ReturnAsync(outputRepositorySearch);

            var useCase = new ListCategories(repositoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Page.Should().Be(outputRepositorySearch.currentPage);
            output.PerPage.Should().Be(outputRepositorySearch.PerPage);
            output.Total.Should().Be(outputRepositorySearch.Total);
            output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
            output.Items.ForEach(outputItem =>
            {
                var repositoryCategory = outputRepositorySearch.Items
                    .Find(x => x.Id == outputItem.Id);

                outputItem.Should().NotBeNull();
                outputItem.Name.Should().Be(repositoryCategory.Name);
                outputItem.Description.Should().Be(repositoryCategory.Description);
                outputItem.IsActive.Should().Be(repositoryCategory.IsActive);
                outputItem.CreateAt.Should().Be(repositoryCategory.CreatedAt);
            });

            repositoryMock.Verify(x => x.Search(
                It.Is<SearchInput>(
                    searchInput.Page == input.Page
                    && searchInput.PerPage = input.PerPage
                    && searchInput.Search == input.Search
                    && searchInput.OrderBy == input.Sort
                    && searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }
    }
}
