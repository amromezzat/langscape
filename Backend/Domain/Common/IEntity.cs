using System;

namespace Domain
{
    /// <summary>
    /// Base class for all entities
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Identifier of the entity
        /// </summary>
        Guid Id { get; set; }
    }
}