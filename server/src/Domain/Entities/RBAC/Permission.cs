

using Domain.Entities.Common;

namespace Domain.Entities.RBAC;

public class Permission: AuditableEntityBase
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required bool IsActive { get; set; } = false;
    Operation Operation { get; set; }
    Resource Resource { get; set; }
}