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
        public DbSet<TShirt> TShirts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
               
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
