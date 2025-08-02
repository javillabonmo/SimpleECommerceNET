using System.ComponentModel.DataAnnotations;

using Domain.Entities.Common;
using Domain.Entities.Inventory;
using Domain.Entities.Sales;

namespace Application.DTOs;
public class RequestProduct 
{   
    Item Item { get; set; }
    public string Name { get; set; } = string.Empty;
    public CategoryResponse Category { get; set; }
    public Guid CategoryId { get; set; }
    uint Stock { get; set; }
    public required decimal Price { get; set; }

    public Product ToProduct()
    {
        return new Product() {Name = Name, Price = Price, Stock = Stock,Category = new Category("a",2),Item = new Item()};

    }
}

public class ProductResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime LastUpdatedAt { get; set; }
    public Guid LastUpdatedBy { get; set; }
    public Item Item { get; set; }
    public string Name { get; set; } = string.Empty;
    public CategoryResponse Category { get; set; }

    public uint Stock { get; set; }
    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]

    public required decimal Price { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not ProductResponse productResponse)
        {
            return false;
        }
        return Id == productResponse.Id &&
               CreatedAt == productResponse.CreatedAt &&
               CreatedBy == productResponse.CreatedBy &&
               LastUpdatedAt == productResponse.LastUpdatedAt &&
               LastUpdatedBy == productResponse.LastUpdatedBy &&
               Item.Equals(productResponse.Item) &&
               Category.Equals(productResponse.Category) &&
               Stock == productResponse.Stock &&
               Price == productResponse.Price;

        //return base.Equals(obj);
    }

    public bool EqualsTo(ProductResponse? obj)//compara dos objetos de tipo ProductResponse especifcmente en sus valores y no en su referencia
    {
        if (obj is not ProductResponse productResponse)
        {
            return false;
        }
        return Id == productResponse.Id &&
               CreatedAt == productResponse.CreatedAt &&
               CreatedBy == productResponse.CreatedBy &&
               LastUpdatedAt == productResponse.LastUpdatedAt &&
               LastUpdatedBy == productResponse.LastUpdatedBy &&
               Item.Equals(productResponse.Item) &&
               Category.Equals(productResponse.Category) &&
               Stock == productResponse.Stock &&
               Price == productResponse.Price;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public override string ToString()
    {
        return $"ProductResponse(Id: {Id}, Name: {Name}, Price: {Price}, Stock: {Stock}, Category: {Category?.Name})";
    }
}

    public static class ProductExtensions 
{
    public static ProductResponse ToProductResponse(this Product product)//convierte el product a productResponse
    {
        return new ProductResponse()
        {
            Id = product.Id,
            CreatedAt = product.CreatedAt,
            CreatedBy = product.CreatedBy,
            LastUpdatedAt = product.LastUpdatedAt,
            LastUpdatedBy = product.LastUpdatedBy,
            Item = product.Item,
            Category = product.Category.ToCategoryResponse(),
            Stock = product.Stock,
            Price = product.Price
        };
    }
}