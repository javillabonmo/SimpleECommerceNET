// <copyright file="Role.cs" company="N/A">
// Copyright (c) N/A. All rights reserved.
// </copyright>
// <author> javillabonmo </author>

using System.ComponentModel.DataAnnotations;

using SimpleEcommerce.Core.Domain.Entities.Common;

namespace SimpleEcommerce.Core.Domain.Entities.AuthDB
{
    /// <summary>
    /// Defines the <see cref="Role" />.
    /// </summary>
    public class Role() : AuditableEntityBase
    {
        /// <summary>
        /// Gets or sets the RoleId.
        /// </summary>
        [Required]
        [Key]
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; } = "Default";

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description { get; set; } = "Default role with minimun access";

        /// <summary>
        /// Gets or sets the CreatedBy.
        /// </summary>


        // public string DeletedToken {get; set;}

        /// <summary>
        /// The ToString
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString() => $"{Name} - {Description}";
    }
}
