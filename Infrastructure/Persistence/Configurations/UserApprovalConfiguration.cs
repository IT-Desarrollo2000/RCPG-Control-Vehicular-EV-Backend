using AutoMapper;
using Domain.Entities.Identity;
using Domain.Entities.User_Approvals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class UserApprovalConfiguration : IEntityTypeConfiguration<UserApproval>
    {
        public void Configure(EntityTypeBuilder<UserApproval> builder)
        {
            builder.HasOne(a => a.Profile)
                .WithMany(p => p.Approvals)
                .HasForeignKey(a => a.ProfileId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
