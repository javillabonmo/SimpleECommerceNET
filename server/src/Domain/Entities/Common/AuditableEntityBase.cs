using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Common;

public abstract class AuditableEntityBase
{
    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid CreatedBy { get; set; } = Guid.Empty;
    public DateTime LastUpdatedAt { get; set; }
    public Guid LastUpdatedBy { get; set; }
}