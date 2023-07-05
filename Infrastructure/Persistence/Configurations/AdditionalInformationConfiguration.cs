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
    public class AdditionalInformationConfiguration : IEntityTypeConfiguration<AdditionalInformation>
    {
        public void Configure(EntityTypeBuilder<AdditionalInformation> builder)
        {

            builder.Property(v => v.MinTurningRadius).HasColumnType("decimal(18,2)");

            builder.Property(v => v.UnladdenMass).HasColumnType("decimal(18,2)");

            builder.Property(v => v.SystemVoltage).HasColumnType("decimal(18,2)");

            builder.Property(v => v.BatteryCapacity).HasColumnType("decimal(18,2)");

            builder.Property(v => v.RatedPower).HasColumnType("decimal(18,2)");

            builder.Property(v => v.MaxSpeed).HasColumnType("decimal(18,2)");

            builder.Property(v => v.MaxCruisingRange).HasColumnType("decimal(18,2)");

            builder.Property(v => v.OperatingCruisingRange).HasColumnType("decimal(18,2)");

            builder.Property(v => v.BrakingDistance).HasColumnType("decimal(18,2)");
        }
    }
}
