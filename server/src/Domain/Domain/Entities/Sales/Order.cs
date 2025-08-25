



namespace SimpleEcommerce.Core.Domain.Entities.Sales
{
    using System.ComponentModel.DataAnnotations;

    using SimpleEcommerce.Core.Domain.Entities.Common;

    public class Order : AuditableEntityBase
    {
        [Key]
        public Guid OrderId { get; set; }

        public int InternalId { get; set; }

        public IEnumerable<OrderProducts> OrderProducts { get; set; }

        public decimal Subtotal { get; set; }

    }
}

