


namespace SimpleEcommerce.Core.Domain.Entities.Sales
{

    using SimpleEcommerce.Core.Domain.Entities.Inventory;
    public class OrderProducts
    {

        public required Product Product { get; set; }

        public int Quantity { get; set; }
    }
}

