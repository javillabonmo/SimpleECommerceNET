// <copyright file="AccountEntityBase.cs" company="N/A">
// Copyright (c) N/A. All rights reserved.
// </copyright>
// <author> javillabonmo </author>

namespace Domain.Entities.Common
{
    using System.ComponentModel.DataAnnotations;

    using Domain.Entities.AuthDB;

    /// <summary>
    /// Defines the <see cref="AccountEntityBase" />.
    /// </summary>
    public abstract class AccountEntityBase : AuditableEntityBase
    {
        /// <summary>
        /// Gets or sets the Role.
        /// </summary>
        public Role Role { get; set; }


        public Guid RoleId { get; set; }
        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        [StringLength(100)]
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        [StringLength(100)]
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the ConfirmPassword.
        /// </summary>
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [StringLength(100)]
        [Required]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsActive.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets the DeactivatedAt.
        /// </summary>
        public DateTime? DeactivatedAt { get; set; }




    }
}
