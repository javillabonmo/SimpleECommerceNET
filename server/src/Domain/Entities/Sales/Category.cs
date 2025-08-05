using Domain.Entities.Common;

namespace Domain.Entities.Sales;

public class Category:  AuditableEntityBase
{
    public string Name { get; set; } = string.Empty;
    public int Discount { get; set; }
}