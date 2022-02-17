using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Core.WatchDogCreator;

public class SourcePreviewCommand
{
    [Required]
    [StringLength(500)]
    public string Url { get; set; }
    
    [Required]
    public HtmlContentOption HtmlContentOption { get; set; }
}