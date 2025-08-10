
using Domain.Entities.Sales;

namespace Application.DTOs;
public class CategoryAddRequest
{
    public string CategoryName { get; set; } = string.Empty;
    public int Discount { get; set; } = 20;
    public Category ToCategory()
    {
        return new Category
        {
            CategoryName = CategoryName,
            Discount = Discount
        };
    }
    
}

public class CategoryResponse
{
    public Guid Id { get; set; }
    public string CategoryName { get; set;}
    public int Discount { get; set; } 
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
    public override bool Equals(object? obj)
    {
        if (obj is not CategoryResponse responseCategory)
        {
            return false;
        }
        return Id == responseCategory.Id &&
               CategoryName == responseCategory.CategoryName &&
               Discount == responseCategory.Discount &&
               CreatedAt == responseCategory.CreatedAt &&
               LastUpdatedAt == responseCategory.LastUpdatedAt;
    }
    

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public override string ToString()
    {
        return $"Id: {Id}, ProductName: {CategoryName}, Discount: {Discount}, CreatedAt: {CreatedAt}, LastUpdatedAt: {LastUpdatedAt}";
    }
    public Category ToCategory()
    {
        return new Category
        {
            Id = Id,
            CategoryName = CategoryName,
            Discount = Discount
        };
    }

}
public class CategoryUpdateRequest : CategoryAddRequest
{
    public Guid Id { get; set; }
    public Category ToCategory()
    {
        return new Category
        {
            Id = Id,
            CategoryName = CategoryName,
            Discount = Discount
        };
    }
}

public static class CategoryExtensions
{
    public static CategoryResponse ToCategoryResponse(this Category category)
    {
        return new CategoryResponse()
        {
            Id = category.Id,
            CategoryName = category.CategoryName,
            Discount = category.Discount,
            CreatedAt = category.CreatedAt,
            LastUpdatedAt = category.LastUpdatedAt
        };
    } 
}