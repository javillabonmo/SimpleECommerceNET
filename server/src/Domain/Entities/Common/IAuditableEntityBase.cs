
namespace Domain.Entities.Common
{
    public interface IAuditableEntityBase
    {
        DateTime CreatedAt { get; set; }


        Guid CreatedBy { get; set; }


        DateTime LastUpdatedAt { get; set; }


        Guid LastUpdatedBy { get; set; }
    }
}

