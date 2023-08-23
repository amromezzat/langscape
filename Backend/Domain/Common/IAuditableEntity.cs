using System;

namespace Domain
{
    /// <summary>
    /// Entity with timestamp and authors data
    /// </summary>
    public interface IAuditableEntity : IEntity
    {
        /// <summary>
        /// Creator id
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// Creation timestamp
        /// </summary>
        DateTime CreatedAt { get; set; }

        /// <summary>
        /// Modifier id
        /// </summary>
        string ModifiedBy { get; set; }

        /// <summary>
        /// Modification timestamp
        /// </summary>
        DateTime? ModifiedAt { get; set; }
    }
}