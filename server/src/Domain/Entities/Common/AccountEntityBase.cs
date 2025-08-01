using Domain.Entities.AuthDB;

namespace Domain.Entities.Common;

public abstract class AccountEntityBase
{
    string? Email { get; set; }
    string? Password { get; set; }
    string? ConfirmPassword { get; set; }
    bool IsActive { get; set; }
    DateTime? DeactivatedAt { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime? ModifiedAt { get; set; }
    
    public required Role Role { get; set; }
}