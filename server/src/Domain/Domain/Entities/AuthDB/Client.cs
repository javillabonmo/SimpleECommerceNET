using SimpleEcommerce.Core.Domain.Entities.Sales;

namespace SimpleEcommerce.Core.Domain.Entities.AuthDB;

public class Client : User
{
    Order Order { get; set; }
}