using System.ComponentModel.DataAnnotations;

using SimpleEcommerce.Core.Domain.Entities.Sales;

namespace SimpleECommerce.Core.DTOs
{
    public class OrderRequest
    {

        public Order ToOrder()
        {
            return new Order { };
        }
    }

    public class OrderResponse
    {
    }

    public class OrderUpdateRequest : OrderRequest
    {
        [Required(ErrorMessage = "OrderId is required")]
        public Guid OrderId { get; set; }
    }


    public static class OrderExtensions
    {

    }
}
