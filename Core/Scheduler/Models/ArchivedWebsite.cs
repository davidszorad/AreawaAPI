using System.ComponentModel.DataAnnotations;
using Core.Shared;

namespace Core.Scheduler
{
    public class ArchivedWebsite
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string SourceUrl { get; set; }
        
        [Required]
        public ArchiveType ArchiveType { get; set; }
    }
}
