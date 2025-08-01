using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.AuthDB;

public class Role(string name, string description)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    private string Name { get; set; } = name;

    [Required]
    private string Description { get; set; } = description;

    public string CreatedBy { get; set; } = ".Net Core";
    public DateTime CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime UpdateAt { get; set; }
    //public string DeletedToken {get; set;}

    public override string ToString() => $"{Name} - {Description}";
    

}