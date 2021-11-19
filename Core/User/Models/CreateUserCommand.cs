using System.ComponentModel.DataAnnotations;

namespace Core.User;

public class CreateUserCommand
{
    [Required]
    [StringLength(255)]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(255)]
    public string LastName { get; set; }
    
    [Required]
    [StringLength(50)]
    [EmailAddress]
    public string Email { get; set; }
}