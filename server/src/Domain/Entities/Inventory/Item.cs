using Domain.Entities.Common;

namespace Domain.Entities.Inventory;


public class Item: AuditableEntityBase
{
    int Stock { get; set; }
    Type Type { get; set; }
    Status Status { get; set; }
}

public class Type : AuditableEntityBase
{
    
}
public class Status : AuditableEntityBase
{
    
}