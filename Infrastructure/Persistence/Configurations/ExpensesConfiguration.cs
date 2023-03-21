using Domain.Entities.Registered_Cars;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ExpensesConfiguration : IEntityTypeConfiguration<Expenses>
    {
        public void Configure(EntityTypeBuilder<Expenses> builder)
        {
            builder.HasOne(ur => ur.TypesOfExpenses)
                    .WithMany(u => u.Expenses)
                    .HasForeignKey(ur => ur.TypesOfExpensesId)
                    .IsRequired();

            builder.HasMany(x => x.Vehicles)
                .WithMany(xr => xr.Expenses);


            builder.HasMany(p => p.PhotosOfSpending)
                .WithOne(pr => pr.Expenses)
                .HasForeignKey(p => p.ExpensesId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.VehicleMaintenanceWorkshop)
                .WithMany(mx => mx.Expenses)
                .HasForeignKey(m => m.VehicleMaintenanceWorkshopId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.VehicleMaintenance)
                .WithMany(ms => ms.Expenses)
                .HasForeignKey(m => m.VehicleMaintenanceId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(v => v.Cost).HasColumnType("decimal(18,2)");
        }
    }
}
