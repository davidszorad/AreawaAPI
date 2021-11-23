using System;
using System.Collections.Generic;
using System.Linq;
using Core.Database.Entities;

namespace Core.CategoriesManagement.Extensions;

internal static class CategoryExtensions
{
    public static ICollection<GetCategoryGroupQuery> Map(this ICollection<Category> entities, ICollection<CategoryGroup> emptyCategoryGroups)
    {
        var result = new List<GetCategoryGroupQuery>();

        foreach (var entity in entities)
        {
            var category = new GetCategoryQuery
            {
                PublicId = entity.PublicId,
                Name = entity.Name,
                UsageCount = 0,
                Created = entity.CreatedOn,
                Updated = entity.UpdatedOn
            };
            
            if (entity.CategoryGroup == null)
            {
                AddCategoryWithNoCategoryGroup(result, category);
            }
            else
            {
                AddCategory(result, entity, category);
            }
        }

        AddEmptyCategoryGroups(emptyCategoryGroups, result);

        return result;
    }

    private static void AddEmptyCategoryGroups(ICollection<CategoryGroup> emptyCategoryGroups, ICollection<GetCategoryGroupQuery> result)
    {
        foreach (var emptyCategoryGroup in emptyCategoryGroups)
        {
            if (result.All(x => x.PublicId != emptyCategoryGroup.PublicId))
            {
                var categoryGroup = new GetCategoryGroupQuery
                {
                    PublicId = emptyCategoryGroup.PublicId,
                    Name = emptyCategoryGroup.Name,
                    Created = emptyCategoryGroup.CreatedOn,
                    Updated = emptyCategoryGroup.UpdatedOn
                };
                result.Add(categoryGroup);
            }
        }
    }

    private static void AddCategory(ICollection<GetCategoryGroupQuery> result, Category entity, GetCategoryQuery category)
    {
        if (result.All(x => x.PublicId != entity.CategoryGroup.PublicId))
        {
            var categoryGroup = new GetCategoryGroupQuery
            {
                PublicId = entity.CategoryGroup.PublicId,
                Name = entity.CategoryGroup.Name,
                Created = entity.CategoryGroup.CreatedOn,
                Updated = entity.CategoryGroup.UpdatedOn
            };
            result.Add(categoryGroup);
        }

        result.Single(x => x.PublicId == entity.CategoryGroup.PublicId).Categories.Add(category);
    }

    private static void AddCategoryWithNoCategoryGroup(ICollection<GetCategoryGroupQuery> result, GetCategoryQuery category)
    {
        if (result.All(x => x.PublicId != null))
        {
            var noCategoryGroup = new GetCategoryGroupQuery
            {
                PublicId = null,
                Name = null,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };
            result.Add(noCategoryGroup);
        }

        result.Single(x => x.PublicId == null).Categories.Add(category);
    }
}