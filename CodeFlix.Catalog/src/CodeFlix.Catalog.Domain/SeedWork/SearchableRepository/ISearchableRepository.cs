namespace CodeFlix.Catalog.Domain.SeedWork.SearchableRepository
{
    public interface ISearchableRepository<TAggragate>
        where TAggragate : AggregateRoot
    {
        Task<SearchOutput<TAggragate>> Search(SearchInput input, CancellationToken cancellationToken);
    }
}
