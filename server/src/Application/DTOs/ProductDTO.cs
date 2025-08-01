using Domain.Entities.Common;
using Domain.Entities.Inventory;
using Domain.Entities.Sales;

namespace Application.DTOs;
public class ProductRequest : AuditableEntityBase
{   
    Item Item { get; set; }
    public string Name { get; set; } = string.Empty;
    Category Category { get; set; }
    int Stock { get; set; }
    public required decimal Price { get; set; }

    public Product ToProduct()
    {
        return new Product() {Name = Name, Price = Price, Stock = Stock,Category = new Category("a",2),Item = new Item()};

    }
}

public class ProductResponse : AuditableEntityBase
{
    public Item Item { get; set; }
    public string Name { get; set; } = string.Empty;
    public Category Category { get; set; }
    public int Stock { get; set; }
    public required decimal Price { get; set; }
}


public static class ProductExtensions 
{
    public static ProductResponse ToResponse(this Product product)//convierte el dto a product
    {
        return new ProductResponse()
        {
            Id = product.Id,
            CreatedAt = product.CreatedAt,
            CreatedBy = product.CreatedBy,
            LastUpdatedAt = product.LastUpdatedAt,
            LastUpdatedBy = product.LastUpdatedBy,
            Item = product.Item,
            Category = product.Category,
            Stock = product.Stock,
            Price = product.Price
        };
    }
}