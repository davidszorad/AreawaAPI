using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Core.WatchDogCreator;

public class CreateWatchDogCommand
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    [Required]
    [StringLength(255)]
    public string Url { get; set; }
    
    [Required]
    public RetryPeriod RetryPeriod { get; set; }
    
    [Required]
    [StringLength(250)]
    public string StartSelector { get; set; }
    
    [StringLength(100)]
    public string EndSelector { get; set; }
}