using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        
        public async Task<Guid> CreateCategoryAsync(UpsertCategoryCommand command, CancellationToken cancellationToken = default)
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

        public async Task<Guid> CreateCategoryGroupAsync(UpsertCategoryGroupCommand command, CancellationToken cancellationToken = default)
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

        public async Task<Guid> UpdateCategoryAsync(Guid publicId, UpsertCategoryCommand command, CancellationToken cancellationToken = default)
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

        public async Task<Guid> UpdateCategoryGroupAsync(Guid publicId, UpsertCategoryGroupCommand command, CancellationToken cancellationToken = default)
        {
            var categoryGroup = await _areawaDbContext.CategoryGroup
                .FirstAsync(x => x.PublicId.Equals(publicId), cancellationToken);

            categoryGroup.Name = command.Name;
            
            await _areawaDbContext.SaveChangesAsync(cancellationToken);

            return categoryGroup.PublicId;
        }

        public async Task<bool> DeleteCategoryAsync(Guid publicId, CancellationToken cancellationToken = default)
        {
            var isCategoryUsed = await _areawaDbContext.WebsiteArchive
                .Include(x => x.WebsiteArchiveCategories)
                .ThenInclude(c => c.Category)
                .Where(x => x.WebsiteArchiveCategories.Any(c => c.Category.PublicId.Equals(publicId)))
                .AnyAsync(cancellationToken);

            if (isCategoryUsed)
            {
                return false;
            }
            
            var category = await _areawaDbContext.Category
                .FirstAsync(x => x.PublicId.Equals(publicId), cancellationToken);

            _areawaDbContext.Category.Remove(category);
            await _areawaDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteCategoryGroupAsync(Guid publicId, CancellationToken cancellationToken = default)
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