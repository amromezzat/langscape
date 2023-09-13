using System;
using Domain.Entities;

namespace Domain
{
    /// <summary>
    /// Entity with timestamp and authors data
    /// </summary>
    public interface IAuditableEntity : IEntity
    {
        /// <summary>
        /// Id of the user that created the entity
        /// </summary>
        string CreatedById { get; set; }

        /// <summary>
        /// User that created the entity
        /// </summary>
        AppUser CreatedBy { get; set; }

        /// <summary>
        /// Creation timestamp
        /// </summary>
        DateTime CreatedAt { get; set; }

        /// <summary>
        /// Id of the user that Modified the entity
        /// </summary>
        string ModifiedById { get; set; }

        /// <summary>
        /// Last user that modified the entity
        /// </summary>
        AppUser ModifiedBy { get; set; }

        /// <summary>
        /// Modification timestamp
        /// </summary>
        DateTime? ModifiedAt { get; set; }
    }
}