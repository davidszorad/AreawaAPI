using System.ComponentModel.DataAnnotations;

namespace Core.CategoriesManagement
{
    public class UpsertCategoryGroupCommand
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}