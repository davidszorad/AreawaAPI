using System;
using Core.Shared;

namespace Core.CategoriesManagement;

public class GetCategoryQuery : BaseQueryResult
{
    public Guid PublicId { get; set; }
    public string Name { get; set; }
    public int UsageCount { get; set; }
}