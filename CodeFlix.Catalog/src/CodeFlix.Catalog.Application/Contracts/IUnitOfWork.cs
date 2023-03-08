namespace CodeFlix.Catalog.Application.Contracts
{
    public interface IUnitOfWork
    {
        public Task Commit(CancellationToken cancellationToken);
    }
}
