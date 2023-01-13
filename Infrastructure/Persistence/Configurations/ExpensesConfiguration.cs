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
    public class ExpensesConfiguration: IEntityTypeConfiguration<Expenses>
    {
        public void Configure(EntityTypeBuilder<Expenses> builder)
        {
            builder.HasOne(ur => ur.TypesOfExpenses)
                    .WithMany(u => u.Expenses)
                    .HasForeignKey(ur => ur.TypesOfExpensesId)
                    .IsRequired();

            builder.HasOne(x => x.Vehicle)
                .WithMany(xr => xr.Expenses)
                .HasForeignKey(x => x.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(p => p.PhotosOfSpending)
                .WithOne(pr => pr.Expenses)
                .HasForeignKey(p => p.ExpensesId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.VehicleMaintenanceWorkshop)
                .WithMany(mx => mx.Expenses)
                .HasForeignKey(m => m.VehicleMaintenanceWorkshopId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(v => v.Cost).HasColumnType("decimal(18,2)");
        }
    }
}
