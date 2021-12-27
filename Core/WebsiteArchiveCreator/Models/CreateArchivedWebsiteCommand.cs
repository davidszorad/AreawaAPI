using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Core.WebsiteArchiveCreator
{
    public class CreateArchivedWebsiteCommand
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        public string SourceUrl { get; set; }

        [Required]
        public ArchiveType ArchiveType { get; set; }
    }
}
