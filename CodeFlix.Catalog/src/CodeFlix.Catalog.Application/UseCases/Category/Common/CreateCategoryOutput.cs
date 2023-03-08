using DomainEntity = CodeFlix.Catalog.Domain.Entity;

namespace CodeFlix.Catalog.Application.UseCases.Category.Common
{
    public class CategoryModelOutput
    {
        public CategoryModelOutput(
            Guid id,
            string name,
            string description,
            bool isActive,
            DateTime createAt)
        {
            Id = id;
            Name = name;
            Description = description;
            IsActive = isActive;
            CreateAt = createAt;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateAt { get; set; }

        public static CategoryModelOutput FromCategory(DomainEntity.Category category)
        {
            return new CategoryModelOutput(category.Id,
                category.Name,
                category.Description,
                category.IsActive,
                category.CreatedAt
            );
        }
    }
}
