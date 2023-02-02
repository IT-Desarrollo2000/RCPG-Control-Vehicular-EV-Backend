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
    public class ChecklistConfiguration : IEntityTypeConfiguration<Checklist> 
    {
        public void Configure(EntityTypeBuilder<Checklist> builder)
        {
            builder.HasMany(ur => ur.VehicleReportUses)
                    .WithOne(u => u.Checklist)
                    .HasForeignKey(ur => ur.ChecklistId)
                    .IsRequired(false);
                    
        }
    }
}
