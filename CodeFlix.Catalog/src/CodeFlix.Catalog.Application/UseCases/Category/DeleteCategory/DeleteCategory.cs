using CodeFlix.Catalog.Application.Contracts;
using CodeFlix.Catalog.Application.Exceptions;
using CodeFlix.Catalog.Domain.Repository;

namespace CodeFlix.Catalog.Application.UseCases.Category.DeleteCategory
{
    public class DeleteCategory : IDeleteCategory
    {
        public readonly ICategoryRepository _categoryRepository;
        public readonly IUnitOfWork _unitOfWork;

        public DeleteCategory(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteCategoryInput request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.Get(request.Id, cancellationToken);

            if(category == null)
            {
                throw new NotFoundException($"Category '{request.Id}' not Found");
            }

            await _categoryRepository.Delete(category, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);
        }
    }
}
