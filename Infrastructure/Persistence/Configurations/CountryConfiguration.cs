using Domain.Entities.Country;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Countries>
    {
        public void Configure(EntityTypeBuilder<Countries> builder)
        {
            builder.HasMany(u => u.States)
                   .WithOne(b => b.Countries)
                   .HasForeignKey(b => b.CountryId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
