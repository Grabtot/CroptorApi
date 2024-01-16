using Croptor.Domain.Presets;
using Croptor.Domain.Users;
using Croptor.Domain.Users.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Croptor.Infrastructure.Persistence
{
    public class CroptorDbContext(DbContextOptions<CroptorDbContext> options)
        : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
    {
        public DbSet<Preset> Presets { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CroptorDbContext).Assembly);


            base.OnModelCreating(modelBuilder);
        }
    }
}
