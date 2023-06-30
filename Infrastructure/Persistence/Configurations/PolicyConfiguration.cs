using Domain.Entities.Registered_Cars;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class PolicyConfiguration : IEntityTypeConfiguration<Policy>
    {
        public void Configure(EntityTypeBuilder<Policy> builder)
        {
            builder.HasMany(ur => ur.PhotosOfPolicies)
                .WithOne(u => u.Policy)
                .HasForeignKey(ur => ur.PolicyId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Vehicle)
                .WithMany(v => v.Policies)
                .HasForeignKey(p => p.VehicleId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(v => v.PolicyCostValue).HasColumnType("decimal(18,2)");
        }
    }
}
