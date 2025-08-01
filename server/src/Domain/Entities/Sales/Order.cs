using Domain.Entities.Common;
using Domain.Entities.Inventory;

namespace Domain.Entities.Sales;

public class Order: AuditableEntityBase
{
    public int OrderId { get; set; }
    public IEnumerable<OrderProducts> OrderProducts  { get; set; }
    
}