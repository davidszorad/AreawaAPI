using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.CategoriesManagement
{
    public interface ICategoriesService
    {
        Task<ICollection<GetCategoryGroupQuery>> GetCategoriesAsync(Guid userPublicId, CancellationToken cancellationToken = default);
        Task<Guid> CreateCategoryAsync(Guid userPublicId, UpsertCategoryCommand command, CancellationToken cancellationToken = default);
        Task<Guid> CreateCategoryGroupAsync(Guid userPublicId, UpsertCategoryGroupCommand command, CancellationToken cancellationToken = default);
        Task<Guid> UpdateCategoryAsync(Guid userPublicId, Guid publicId, UpsertCategoryCommand command, CancellationToken cancellationToken = default);
        Task<Guid> UpdateCategoryGroupAsync(Guid userPublicId, Guid publicId, UpsertCategoryGroupCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteCategoryAsync(Guid userPublicId, Guid publicId, CancellationToken cancellationToken = default);
        Task<bool> DeleteCategoryGroupAsync(Guid userPublicId, Guid publicId, CancellationToken cancellationToken = default);
    }
}