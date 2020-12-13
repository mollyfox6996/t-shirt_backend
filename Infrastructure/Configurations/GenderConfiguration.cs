using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.HasData
            (
                new Gender[]
                {
                    new Gender {Id = 1, Name = "Male"},
                    new Gender {Id = 2, Name = "Female"}
                }
            );
        }
    }
}
