using Domain.Entities.Common;
using Domain.Entities.Sales;

namespace Domain.Entities.Inventory;

///<summary>
/// Represents a product in the inventory. 
///</summary>
public class Product : AuditableEntityBase
{
    public Item Item { get; set; }
    public int ItemId { get; set; } 
    public string ProductName { get; set; }
    public Category? Category { get; set; }
    public Guid CategoryId { get; set; }
    public uint Stock { get; set; }
    public required decimal Price { get; set; }
}