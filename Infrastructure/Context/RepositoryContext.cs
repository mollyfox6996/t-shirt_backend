using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Context
{
    public class RepositoryContext : IdentityDbContext<AppUser>
    {
        public DbSet<CustomBasket> CustomBaskets { get; set; }
        public DbSet<BasketItem> Items { get; set; }
        public DbSet<TShirt> TShirts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) 
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new GenderConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            
            foreach(var item in builder.Model.GetEntityTypes())
            {
                var props = item.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                foreach(var prop in props)
                {
                    builder.Entity(item.Name).Property(prop.Name).HasConversion<double>();
                }
            }
        }
    }
}
