namespace CodeFlix.Catalog.Domain.SeedWork.SearchableRepository
{
    public class SearchOutput<TAggragate> 
        where TAggragate : AggregateRoot
    {
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public IReadOnlyList<TAggragate> Items { get; set; }

        public SearchOutput(
            int currentPage, 
            int perPage, 
            int total, 
            IReadOnlyList<TAggragate> items)
        {
            CurrentPage = currentPage;
            PerPage = perPage;
            Total = total;
            Items = items;
        }
    }
}
