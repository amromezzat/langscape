using System;
using Domain.Entities;

namespace Domain.Common.Implementation
{
    public abstract class AuditableEntityBase : IAuditableEntity
    {
        public Guid Id { get; set; }
        public string CreatedById { get; set; }
        public AppUser CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifiedById { get; set; }
        public AppUser ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}