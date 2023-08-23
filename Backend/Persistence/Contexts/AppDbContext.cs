using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<FlashCardSet> FlashCardSets { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}