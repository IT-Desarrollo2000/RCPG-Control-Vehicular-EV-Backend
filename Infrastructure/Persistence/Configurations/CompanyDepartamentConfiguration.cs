using Domain.Entities.Company;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CompanyDepartamentConfiguration : IEntityTypeConfiguration<Companies>
    {
        public void Configure(EntityTypeBuilder<Companies> builder)
        {
            builder.HasMany(ur => ur.Departaments)
               .WithOne(u => u.Company)
               .HasForeignKey(ur => ur.CompanyId)
               .IsRequired();
        }
    }
}
