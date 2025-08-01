using Domain.Entities.Common;
using Domain.Entities.Sales;

namespace Domain.Entities.Inventory;

///<summary>
/// Represents a product in the inventory. 
///</summary>
public class Product : AuditableEntityBase
{
    public Item Item { get; set; }
    public string Name { get; set; } = string.Empty;
    public Category Category { get; set; }
    public int Stock { get; set; }
    public required decimal Price { get; set; }
}