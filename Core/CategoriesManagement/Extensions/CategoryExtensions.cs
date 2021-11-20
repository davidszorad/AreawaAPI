namespace Core.CategoriesManagement.Extensions;

internal static class CategoryExtensions
{
    public static GetCategoryQuery Map(this Database.Entities.Category entity)
    {
        return new GetCategoryQuery
        {
            PublicId = entity.PublicId,
            Name = entity.Name,
            Group = entity.CategoryGroup?.Name,
            Created = entity.CreatedOn,
            Updated = entity.UpdatedOn
        };
    }
}