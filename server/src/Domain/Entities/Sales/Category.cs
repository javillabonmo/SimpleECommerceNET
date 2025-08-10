using Domain.Entities.Common;

namespace Domain.Entities.Sales;

public class Category:  AuditableEntityBase
{
    public string CategoryName { get; set; }
    public int Discount { get; set; }
}