namespace Domain.Entities.Common;

public abstract class AuditableEntityBase
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid CreatedBy { get; set; } = Guid.Empty;
    public DateTime LastUpdatedAt { get; set; }
    public Guid LastUpdatedBy { get; set; }
}