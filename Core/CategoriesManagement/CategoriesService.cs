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

        public async Task<ICollection<GetCategoryGroupQuery>> GetCategoriesAsync(Guid userPublicId, CancellationToken cancellationToken = default)
        {
            // TODO: include category groups with no categories
            
            var categories = await _areawaDbContext
                .Category
                .Include(x => x.ApiUser)
                .Include(x => x.CategoryGroup)
                .ThenInclude(g => g.ApiUser)
                .Where(x => x.ApiUser.IsActive && x.ApiUser.PublicId == userPublicId)
                .Where(x => x.CategoryGroup == null || (x.CategoryGroup.ApiUser.IsActive && x.CategoryGroup.ApiUser.PublicId == userPublicId))
                .ToListAsync(cancellationToken);

            return categories.Map();
        }

        public async Task<Guid> CreateCategoryAsync(Guid userPublicId, UpsertCategoryCommand command, CancellationToken cancellationToken = default)
        {
            var apiUser = await _areawaDbContext.ApiUser.FirstAsync(x => x.PublicId == userPublicId, cancellationToken);
            
            var category = new Category
            {
                Name = command.Name,
                PublicId = Guid.NewGuid(),
                ApiUser = apiUser
            };

            if (command.CategoryGroupPublicId.HasValue)
            {
                var categoryGroup = await _areawaDbContext.CategoryGroup
                    .FirstAsync(x => x.PublicId.Equals(command.CategoryGroupPublicId.Value) && x.ApiUserId == apiUser.ApiUserId, cancellationToken);

                category.CategoryGroup = categoryGroup;
            }

            _areawaDbContext.Category.Add(category);
            await _areawaDbContext.SaveChangesAsync(cancellationToken);

            return category.PublicId;
        }

        public async Task<Guid> CreateCategoryGroupAsync(Guid userPublicId, UpsertCategoryGroupCommand command, CancellationToken cancellationToken = default)
        {
            var apiUser = await _areawaDbContext.ApiUser.FirstAsync(x => x.PublicId == userPublicId, cancellationToken);
            
            var categoryGroup = new CategoryGroup
            {
                Name = command.Name,
                PublicId = Guid.NewGuid(),
                ApiUser = apiUser
            };

            _areawaDbContext.CategoryGroup.Add(categoryGroup);
            await _areawaDbContext.SaveChangesAsync(cancellationToken);

            return categoryGroup.PublicId;
        }

        public async Task<Guid> UpdateCategoryAsync(Guid userPublicId, Guid publicId, UpsertCategoryCommand command, CancellationToken cancellationToken = default)
        {
            var apiUser = await _areawaDbContext.ApiUser.FirstAsync(x => x.PublicId == userPublicId, cancellationToken);
            
            var category = await _areawaDbContext.Category
                .FirstAsync(x => x.PublicId.Equals(publicId) && x.ApiUserId == apiUser.ApiUserId, cancellationToken);

            category.Name = command.Name;
            category.CategoryGroup = null;

            if (command.CategoryGroupPublicId.HasValue)
            {
                var categoryGroup = await _areawaDbContext.CategoryGroup
                    .FirstAsync(x => x.PublicId.Equals(command.CategoryGroupPublicId.Value) && x.ApiUserId == apiUser.ApiUserId, cancellationToken);

                category.CategoryGroup = categoryGroup;
            }
            
            await _areawaDbContext.SaveChangesAsync(cancellationToken);

            return category.PublicId;
        }

        public async Task<Guid> UpdateCategoryGroupAsync(Guid userPublicId, Guid publicId, UpsertCategoryGroupCommand command, CancellationToken cancellationToken = default)
        {
            var apiUser = await _areawaDbContext.ApiUser.FirstAsync(x => x.PublicId == userPublicId, cancellationToken);
            
            var categoryGroup = await _areawaDbContext.CategoryGroup
                .FirstAsync(x => x.PublicId.Equals(publicId) && x.ApiUserId == apiUser.ApiUserId, cancellationToken);

            categoryGroup.Name = command.Name;
            
            await _areawaDbContext.SaveChangesAsync(cancellationToken);

            return categoryGroup.PublicId;
        }

        public async Task<bool> DeleteCategoryAsync(Guid userPublicId, Guid publicId, CancellationToken cancellationToken = default)
        {
            var apiUser = await _areawaDbContext.ApiUser.FirstAsync(x => x.PublicId == userPublicId, cancellationToken);
            
            var category = await _areawaDbContext.Category
                .Include(x => x.WebsiteArchiveCategories)
                .FirstAsync(x => x.PublicId.Equals(publicId) && x.ApiUserId == apiUser.ApiUserId, cancellationToken);

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
            var apiUser = await _areawaDbContext.ApiUser.FirstAsync(x => x.PublicId == userPublicId, cancellationToken);
            
            var categoryGroup = await _areawaDbContext.CategoryGroup
                .Include(x => x.Categories)
                .FirstAsync(c => c.PublicId.Equals(publicId) && c.ApiUserId == apiUser.ApiUserId, cancellationToken);

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