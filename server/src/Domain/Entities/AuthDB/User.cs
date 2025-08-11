

namespace Domain.Entities.AuthDB
{
    using System.ComponentModel.DataAnnotations;

    using Domain.Entities.Common;

    public class User : AccountEntityBase
    {
        [Key]
        required public Guid Id { get; set; } = Guid.NewGuid();

        public string? Username { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public override string ToString()
        {
            return $"{Username} - {FirstName} {LastName} ({Email})";
        }
    }
}