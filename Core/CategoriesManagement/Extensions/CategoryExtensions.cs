using System.Collections.Generic;
using System.Linq;

namespace Core.CategoriesManagement.Extensions;

internal static class CategoryExtensions
{
    public static GetCategoryGroupQuery Map(this Database.Entities.CategoryGroup entity)
    {
        return new GetCategoryGroupQuery
        {
            PublicId = entity.PublicId,
            Name = entity.Name,
            Created = entity.CreatedOn,
            Updated = entity.UpdatedOn,
            Categories = entity.Categories.Select(x => x.Map()).ToList()
        };
    }
    
    public static ICollection<GetCategoryGroupQuery> Map(this ICollection<Database.Entities.Category> entities)
    {
        var result = new List<GetCategoryGroupQuery>();

        var noCategoryGroup = new GetCategoryGroupQuery
        {
            PublicId = null,
            Name = null
        };
        result.Add(noCategoryGroup);

        foreach (var entity in entities)
        {
            var category = new GetCategoryQuery
            {
                PublicId = entity.PublicId,
                Name = entity.Name,
                UsageCount = 0
            };
            
            if (entity.CategoryGroup == null)
            {
                result.Single(x => x.PublicId == null).Categories.Add(category);
            }
        }


        return result;
    }
    
    private static GetCategoryQuery Map(this Database.Entities.Category entity)
    {
        return new GetCategoryQuery
        {
            PublicId = entity.PublicId,
            Name = entity.Name,
            UsageCount = 0,
            Created = entity.CreatedOn,
            Updated = entity.UpdatedOn
        };
    }
}