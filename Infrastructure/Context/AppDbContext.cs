using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using System.Linq;

namespace Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<CustomBasket> CustomBaskets { get; set; }
        public DbSet<BasketItem> Items { get; set; }
        public DbSet<TShirt> TShirts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().HasData
                (
                    new Category[]
                    {
                        new Category {Id = 1, Name = "Humor"},
                        new Category {Id = 2, Name = "IT"},
                        new Category {Id = 3, Name = "Animals"},
                        new Category {Id = 4, Name = "Other"}
                    }
                );
            builder.Entity<Gender>().HasData
                (
                    new Gender[]
                    {
                        new Gender {Id = 1, Name = "Male"},
                        new Gender {Id = 2, Name = "Female"}
                    }
                );
            
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
