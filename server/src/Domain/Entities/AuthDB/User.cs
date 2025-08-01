using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.AuthDB;

public class User
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    [Compare("Password", ErrorMessage =  "The password and confirmation password do not match.")]
    public string? ConfirmPassword { get; set; } 
    public bool IsActive { get; set; } = true;
    public DateTime? DeactivatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Role Role { get; set; } = new Role("generic_user", ".NET core - generic_user");
}