using System;
using System.ComponentModel.DataAnnotations;

namespace Core.CategoriesManagement
{
    public class UpsertCategoryCommand
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public Guid? CategoryGroupPublicId { get; set; }
    }
}