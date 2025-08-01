using Domain.Entities.Common;
using Domain.Entities.Inventory;

namespace Domain.Entities.Sales;

public class OrderProducts
{
    
    public required Product Product { get; set; }

    public int Quantity { get; set; }
}