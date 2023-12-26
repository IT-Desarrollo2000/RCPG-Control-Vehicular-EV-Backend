using Application.Interfaces;
using Azure.Storage.Blobs;
using Domain.CustomEntities;
using Domain.Entities.Identity;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.BlobStorage;
using Infrastructure.Filters;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //Validators
            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            });

            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

            services.AddValidatorsFromAssemblyContaining<UserRegistrationRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<TokenRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<ProfileImageValidator>();


            //Azure blob storage
            services.AddScoped(options =>
            {
                //Desarrollo
                return new BlobServiceClient(configuration.GetConnectionString("DebugStorage"));
            });

            services.AddScoped<IBlobStorageService, BlobStorageService>();
            services.Configure<BlobContainers>(configuration.GetSection("BlobContainers"));

            //Identity
            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
                opt.User.RequireUniqueEmail = true;
            })
            .AddRoles<AppRole>()
                .AddRoleManager<RoleManager<AppRole>>()
                .AddSignInManager<SignInManager<AppUser>>()
            .AddRoleValidator<RoleValidator<AppRole>>()
                .AddEntityFrameworkStores<CVContext>()
                .AddDefaultTokenProviders();

            //Context and Repositories
            ///PRODUCTION
            //services.AddDbContext<CVContext>(options => options.UseSqlServer(configuration.GetConnectionString("AzureDB")));
            ///DEBUG
            services.AddDbContext<CVContext>(options => options.UseSqlServer(configuration.GetConnectionString("DebugDB")));
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Services
            services.AddTransient<IIdentityService, IdentityService>();

            //Pagination
            services.Configure<PaginationOptions>(configuration.GetSection("Pagination"));

            //Loop Reference Handler
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                //options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            //Automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
