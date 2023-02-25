using Domain.Entities.Departament;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Departaments>
    {
        public void Configure(EntityTypeBuilder<Departaments> builder)
        {
            builder.HasMany(d => d.Supervisors)
                .WithMany(u => u.AssignedDepartments);
        }
    }
}
