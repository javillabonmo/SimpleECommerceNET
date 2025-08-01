using Domain.Entities.Common;

namespace Domain.Entities.RBAC;

public class Policy: AuditableEntityBase
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    bool IsPublic { get; set; }
    string Functionality { get; set; }
    bool IsActive { get; set; }
}