using Domain.Entities.Common;

namespace Domain.Entities.RBAC;

public class Operation: AuditableEntityBase
{
    public required string Name { get; set; }
    public required string Description { get; set; }
}