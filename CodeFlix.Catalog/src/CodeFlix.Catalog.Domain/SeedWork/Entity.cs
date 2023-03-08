namespace CodeFlix.Catalog.Domain.SeedWork
{
    public class Entity
    {
        public Guid Id { get; set; }

        protected Entity() => Id = Guid.NewGuid();
    }
}
