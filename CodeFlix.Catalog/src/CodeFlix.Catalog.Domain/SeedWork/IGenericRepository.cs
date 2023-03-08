namespace CodeFlix.Catalog.Domain.SeedWork
{
    public interface IGenericRepository<TAggregate> : IRepository
    {
        Task Insert(TAggregate aggregate, CancellationToken cancellation);
        Task<TAggregate> Get(Guid id, CancellationToken cancellation);
    }
}
