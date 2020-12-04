using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData
            (
                new Category[]
                {
                    new Category {Id = 1, Name = "Humor"},
                    new Category {Id = 2, Name = "IT"},
                    new Category {Id = 3, Name = "Animals"},
                    new Category {Id = 4, Name = "Other"}
                }
            );
        }
    }
}
