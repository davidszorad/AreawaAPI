using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.CategoriesManagement
{
    public interface ICategoriesService
    {
        Task<Guid> CreateCategoryAsync(UpsertCategoryCommand command, CancellationToken cancellationToken = default);
        Task<Guid> CreateCategoryGroupAsync(UpsertCategoryGroupCommand command, CancellationToken cancellationToken = default);
        Task<Guid> UpdateCategoryAsync(Guid publicId, UpsertCategoryCommand command, CancellationToken cancellationToken = default);
        Task<Guid> UpdateCategoryGroupAsync(Guid publicId, UpsertCategoryGroupCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteCategoryAsync(Guid publicId, CancellationToken cancellationToken = default);
        Task<bool> DeleteCategoryGroupAsync(Guid publicId, CancellationToken cancellationToken = default);
    }
}