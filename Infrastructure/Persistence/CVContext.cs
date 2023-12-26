using Domain.Entities.Company;
using Domain.Entities.Country;
using Domain.Entities.Departament;
using Domain.Entities.Identity;
using Domain.Entities.Municipality;
using Domain.Entities.Profiles;
using Domain.Entities.Propietary;
using Domain.Entities.Registered_Cars;
using Domain.Entities.State;
using Domain.Entities.User_Approvals;
using Infrastructure.Persistence.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ess;

namespace Infrastructure.Persistence
{
    public class CVContext : IdentityDbContext<AppUser, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public CVContext(DbContextOptions<CVContext> options) : base(options)
        {

        }

        ///DECLARACIÖN DE ENTIDADES
        //Entidades Base
        //Security
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<AppUserSocial> AppUserSocials { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }

        //Companies
        public virtual DbSet<Companies> Companies { get; set; }
        //Departamens
        public virtual DbSet<Departaments> Departaments { get; set; }

        //User Approvals
        public virtual DbSet<UserApproval> UserApprovals { get; set; }

        //Vehicles
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleImage> VehicleImages { get; set; }

        //VehicleService
        public virtual DbSet<VehicleService> VehicleServices { get; set; }

        //Registered Cars
        public virtual DbSet<Checklist> Checklists { get; set; }
        public virtual DbSet<Expenses> Expenses { get; set; }
        public virtual DbSet<TypesOfExpenses> TypesOfExpenses { get; set; }
        public virtual DbSet<PhotosOfSpending> PhotosOfSpendings { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<AdditionalInformation> VehicleAdditionalInfo { get; set; }


        //Vehicle Maintenance With Vehicle MaintenanceWorkShop
        public virtual DbSet<VehicleMaintenance> VehicleMaintenances { get; set; }
        public virtual DbSet<MaintenanceProgress> MaintenanceProgresses { get; set; }
        public virtual DbSet<VehicleMaintenanceWorkshop> VehicleMaintenanceWorkshops { get; set; }
        public virtual DbSet<MaintenanceProgressImages> MaintenanceProgressImages { get; set; }

        //Vehicle Report with Image
        public virtual DbSet<VehicleReport> VehicleReports { get; set; }
        public virtual DbSet<VehicleReportImage> VehicleReportImages { get; set; }

        //VehicleReportUse and Destinations
        public virtual DbSet<VehicleReportUse> VehicleReportUses { get; set; }
        public virtual DbSet<DestinationOfReportUse> DestinationOfReportUses { get; set; }

        //Policy
        public virtual DbSet<Policy> Policy { get; set; }

        //Propietary
        public virtual DbSet<Propietary> Propietaries { get; set; }

        //Country
        public virtual DbSet<Countries> Countries { get; set; }

        //State
        public virtual DbSet<States> States { get; set; }

        //Municipality
        public virtual DbSet<Municipalities> Municipalities { get; set; }

        //Tenency
        public virtual DbSet<VehicleTenency> VehicleTenencies { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.ApplyConfiguration(new AppRoleConfiguration());
            builder.ApplyConfiguration(new UserApprovalConfiguration());
            builder.ApplyConfiguration(new CompanyDepartamentConfiguration());
            builder.ApplyConfiguration(new VehicleConfiguration());
            builder.ApplyConfiguration(new VehicleImageConfiguration());
            builder.ApplyConfiguration(new ExpensesConfiguration());
            builder.ApplyConfiguration(new VehicleMaintenanceConfiguration());
            builder.ApplyConfiguration(new UserProfileConfiguration());
            builder.ApplyConfiguration(new ChecklistConfiguration());
            builder.ApplyConfiguration(new VehicleReportConfiguration());
            builder.ApplyConfiguration(new VehicleReportUseConfiguration());
            builder.ApplyConfiguration(new DestinationOfReportUseConfiguration());
            builder.ApplyConfiguration(new DepartmentConfiguration());
            builder.ApplyConfiguration(new VehicleServiceConfiguration());
            builder.ApplyConfiguration(new MaintenanceConfiguration());
            builder.ApplyConfiguration(new MaintenanceProgressConfiguration());
            builder.ApplyConfiguration(new ProgressImagesConfig());
            builder.ApplyConfiguration(new AdditionalInformationConfiguration());
            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new StateConfiguration());
            builder.ApplyConfiguration(new VehicleTenencyConfiguration());
            builder.ApplyConfiguration(new PolicyConfiguration());
        }
    }
}
