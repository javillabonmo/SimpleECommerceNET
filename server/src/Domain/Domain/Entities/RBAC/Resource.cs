using SimpleEcommerce.Core.Domain.Entities.Common;

namespace SimpleEcommerce.Core.Domain.Entities.RBAC;

public class Resource: AuditableEntityBase
{
    public required string Name { get; set; }
}