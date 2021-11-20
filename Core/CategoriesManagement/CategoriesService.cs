using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.CategoriesManagement.Extensions;
using Core.Database;
using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.CategoriesManagement
{
    public class CategoriesService : ICategoriesService
    {
        private readonly AreawaDbContext _areawaDbContext;

        public CategoriesService(AreawaDbContext areawaDbContext)
        {
            _areawaDbContext = areawaDbContext;
        }

        public async Task<ICollection<GetCategoryQuery>> GetCategoriesAsync(Guid userPublicId, CancellationToken cancellationToken = default)
        {
            var categories = await _areawaDbContext
                .Category
                .Include(x => x.CategoryGroup)
                .Include(x => x.WebsiteArchiveCategories)
                .ToListAsync(cancellationToken: cancellationToken);

            return categories.ConvertAll(x => x.Map());
        }

        public async Task<Guid> CreateCategoryAsync(Guid userPublicId, UpsertCategoryCommand command, CancellationToken cancellationToken = default)
        {
            var category = new Category
            {
                Name = command.Name,
                PublicId = Guid.NewGuid()
            };

            if (command.CategoryGroupPublicId.HasValue)
            {
                var categoryGroup = await _areawaDbContext.CategoryGroup
                    .FirstAsync(x => x.PublicId.Equals(command.CategoryGroupPublicId.Value), cancellationToken);

                category.CategoryGroup = categoryGroup;
            }

            _areawaDbContext.Category.Add(category);
            await _areawaDbContext.SaveChangesAsync(cancellationToken);

            return category.PublicId;
        }

        public async Task<Guid> CreateCategoryGroupAsync(Guid userPublicId, UpsertCategoryGroupCommand command, CancellationToken cancellationToken = default)
        {
            var categoryGroup = new CategoryGroup
            {
                Name = command.Name,
                PublicId = Guid.NewGuid()
            };

            _areawaDbContext.CategoryGroup.Add(categoryGroup);
            await _areawaDbContext.SaveChangesAsync(cancellationToken);

            return categoryGroup.PublicId;
        }

        public async Task<Guid> UpdateCategoryAsync(Guid userPublicId, Guid publicId, UpsertCategoryCommand command, CancellationToken cancellationToken = default)
        {
            var category = await _areawaDbContext.Category
                .FirstAsync(x => x.PublicId.Equals(publicId), cancellationToken);

            category.Name = command.Name;
            category.CategoryGroup = null;

            if (command.CategoryGroupPublicId.HasValue)
            {
                var categoryGroup = await _areawaDbContext.CategoryGroup
                    .FirstAsync(x => x.PublicId.Equals(command.CategoryGroupPublicId.Value), cancellationToken);

                category.CategoryGroup = categoryGroup;
            }
            
            await _areawaDbContext.SaveChangesAsync(cancellationToken);

            return category.PublicId;
        }

        public async Task<Guid> UpdateCategoryGroupAsync(Guid userPublicId, Guid publicId, UpsertCategoryGroupCommand command, CancellationToken cancellationToken = default)
        {
            var categoryGroup = await _areawaDbContext.CategoryGroup
                .FirstAsync(x => x.PublicId.Equals(publicId), cancellationToken);

            categoryGroup.Name = command.Name;
            
            await _areawaDbContext.SaveChangesAsync(cancellationToken);

            return categoryGroup.PublicId;
        }

        public async Task<bool> DeleteCategoryAsync(Guid userPublicId, Guid publicId, CancellationToken cancellationToken = default)
        {
            var category = await _areawaDbContext.Category
                .Include(x => x.WebsiteArchiveCategories)
                .FirstAsync(x => x.PublicId.Equals(publicId), cancellationToken);

            if (category.WebsiteArchiveCategories.Any())
            {
                return false;
            }

            _areawaDbContext.Category.Remove(category);
            await _areawaDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteCategoryGroupAsync(Guid userPublicId, Guid publicId, CancellationToken cancellationToken = default)
        {
            var categoryGroup = await _areawaDbContext.CategoryGroup
                .Include(x => x.Categories)
                .FirstAsync(c => c.PublicId.Equals(publicId), cancellationToken);

            if (categoryGroup.Categories.Count > 0)
            {
                return false;
            }
            
            _areawaDbContext.CategoryGroup.Remove(categoryGroup);
            await _areawaDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}