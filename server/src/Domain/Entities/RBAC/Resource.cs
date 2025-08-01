using Domain.Entities.Common;

namespace Domain.Entities.RBAC;

public class Resource: AuditableEntityBase
{
    public required string Name { get; set; }
}