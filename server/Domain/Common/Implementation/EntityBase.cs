using System;

namespace Domain.Common.Implementation
{
    public class EntityBase : IEntity
    {
        public Guid Id { get; set; }
    }
}