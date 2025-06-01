using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Domain.Entities;

namespace Pizzeria.Infrastructure.Identities
{
    public class ApplicationUserContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationUserContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ingredient>()
               .HasMany(i => i.Items)
               .WithMany(p => p.Ingredients);

            modelBuilder.Entity<Item>()
               .HasMany(p => p.Orders)
               .WithMany(o => o.Items);
        }
    }
}
