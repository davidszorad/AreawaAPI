using System.ComponentModel.DataAnnotations;

namespace Core.ChangeTracker;

public class SourcePreviewCommand
{
    [Required]
    [StringLength(500)]
    public string Url { get; set; }
}