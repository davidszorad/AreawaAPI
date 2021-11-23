using System;
using System.Collections.Generic;
using Core.Shared;

namespace Core.CategoriesManagement;

public class GetCategoryGroupQuery : BaseItemResult
{
    public Guid? PublicId { get; set; }
    public string Name { get; set; }
    
    public ICollection<GetCategoryQuery> Categories { get; set; }

    public GetCategoryGroupQuery()
    {
        Categories = new List<GetCategoryQuery>();
    }
}