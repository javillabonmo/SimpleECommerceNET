using Domain.Entities.Common;

namespace Domain.Entities.Sales;

public class Category(string name, int discount = 0) :  AuditableEntityBase
{
    public string Name { get; set; } = name;
    public int Discount { get; set; } = discount;
}