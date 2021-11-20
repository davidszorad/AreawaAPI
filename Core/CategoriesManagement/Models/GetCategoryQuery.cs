using System;
using Core.Shared;

namespace Core.CategoriesManagement;

public class GetCategoryQuery : BaseItemResult
{
    public Guid PublicId { get; set; }
    public string Name { get; set; }
    public string Group { get; set; }
}