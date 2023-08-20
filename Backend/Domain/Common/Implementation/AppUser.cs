
using Microsoft.AspNetCore.Identity;

namespace Domain.Implementation
{
    public class AppUser : IdentityUser, IAuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string DisplayName { get; set; }
    }
}