
using Domain.Entities.Sales;

namespace Application.DTOs;
public class CategoryAddRequest
{
    public string Name { get; set; } = string.Empty;
    public int Discount { get; set; } = 20;
    public Category ToCategory()
    {
        return new Category
        {
            Name = Name,
            Discount = Discount
        };
    }
    
}

public class CategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Discount { get; set; } = 20;
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
    public override bool Equals(object? obj)
    {
        if (obj is not CategoryResponse responseCategory)
        {
            return false;
        }
        return Id == responseCategory.Id &&
               Name == responseCategory.Name &&
               Discount == responseCategory.Discount &&
               CreatedAt == responseCategory.CreatedAt &&
               LastUpdatedAt == responseCategory.LastUpdatedAt;
    }
    public bool EqualsTo(CategoryResponse? obj)//compara dos objetos de tipo CategoryResponse especifcamente en sus valores y no en su referencia
    {
        if (obj is not CategoryResponse responseCategory)
        {
            return false;
        }
        return Id == responseCategory.Id &&
               Name == responseCategory.Name &&
               Discount == responseCategory.Discount &&
               CreatedAt == responseCategory.CreatedAt &&
               LastUpdatedAt == responseCategory.LastUpdatedAt;
    }
    public CategoryAddRequest ToRequestCategory()
    {
        return new CategoryAddRequest()
        {
            Name = Name,
            Discount = Discount
        };
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Discount: {Discount}, CreatedAt: {CreatedAt}, LastUpdatedAt: {LastUpdatedAt}";
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
            Name = Name,
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
            Name = category.Name,
            Discount = category.Discount,
            CreatedAt = category.CreatedAt,
            LastUpdatedAt = category.LastUpdatedAt
        };
    } 
}