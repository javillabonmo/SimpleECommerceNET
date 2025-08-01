using Domain.Entities.Sales;

namespace Domain.Entities.AuthDB;

public class Client : User
{
    Order  Order { get; set; }
}