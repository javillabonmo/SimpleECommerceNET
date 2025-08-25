using SimpleEcommerce.Core.Domain.Entities.Common;

namespace SimpleEcommerce.Core.Domain.Entities.RBAC;

public class Operation: AuditableEntityBase
{
    public required string Name { get; set; }
    public required string Description { get; set; }
}