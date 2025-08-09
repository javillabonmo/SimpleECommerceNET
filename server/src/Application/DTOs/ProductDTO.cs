using System.ComponentModel.DataAnnotations;

using Domain.Entities.Common;
using Domain.Entities.Inventory;
using Domain.Entities.Sales;

namespace Application.DTOs;
public class ProductRequest 
{   
    Item Item { get; set; }
    [DataType(DataType.Text)]
    public string ProductName { get; set; } = string.Empty;
    public Category? Category { get; set; }
    public Guid CategoryId { get; set; }
    
    public uint Stock { get; set; }
    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
    [DataType(DataType.Currency)]
    public required decimal Price { get; set; }

    public Product ToProduct()
    {
        return new Product() {ProductName = ProductName, Price = Price, Stock = Stock, Category = Category,CategoryId = CategoryId,Item = new Item()};

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
    public string ProductName { get; set; }
    public Category Category { get; set; }
    public Guid CategoryId { get; set; }

    public uint Stock { get; set; }
    

    public required decimal Price { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not ProductResponse productResponse)
        {
            return false;
        }
        return Id == productResponse.Id &&
            ProductName == productResponse.ProductName &&
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

    
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public override string ToString()
    {
        return $"ProductResponse(Id: {Id}, ProductName: {ProductName}, Price: {Price}, Stock: {Stock}, Category: {Category?.CategoryName})";
    }
    public ProductUpdateRequest ToProductUpdateRequest()
    {
        return new ProductUpdateRequest()
        {
            Id = Id,
            ProductName = ProductName,
            Category = Category,
            CategoryId = CategoryId,
            Stock = Stock,
            Price = Price
        };
    }
    public Product ToProduct()
    {
        return new Product() { Id = Id, ProductName = ProductName, Price = Price, Stock = Stock, Category = Category,CategoryId = CategoryId, Item = Item };

    }
}

public static class ProductExtensions 
{
    public static ProductResponse ToProductResponse(this Product product)//convierte el product a productResponse
    {
        return new ProductResponse()
        {
            Id = product.Id,
            ProductName = product.ProductName,
            CreatedAt = product.CreatedAt,
            CreatedBy = product.CreatedBy,
            LastUpdatedAt = product.LastUpdatedAt,
            LastUpdatedBy = product.LastUpdatedBy,
            Item = product.Item,
            Category = product.Category,
            CategoryId = product.CategoryId,
            Stock = product.Stock,
            Price = product.Price
        };
    }
}

public class ProductUpdateRequest
{
    [Required(ErrorMessage = "Id is required")]
    public Guid Id { get; set; }
    public string? ProductName { get; set; }
    public Category? Category { get; set; }
    public Guid CategoryId { get; set; }
    public uint? Stock { get; set; }
    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
    public decimal? Price { get; set; }

    public Product ToProduct()
    {
        return new Product()
        {
            Id = Id,
            ProductName = ProductName ?? string.Empty,
            Price = Price ?? 0,
            Stock = Stock ?? 0,
            Category = Category,
            CategoryId = CategoryId,
            // Handle null Category
            Item = new Item() // Assuming a default item if not provided
        };
    }
}