using Azure.Storage.Blobs;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using Infrastructure.Filters;
using Application.Interfaces;
using Infrastructure.BlobStorage;
using Domain.CustomEntities;
using Domain.Entities.Identity;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Identity;
using Infrastructure.Validators;
using FluentValidation;

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
            //services.AddValidatorsFromAssemblyContaining<AccountsGroupRequestValidator>();


            //Azure blob storage
            services.AddScoped(options =>
            {
                return new BlobServiceClient(configuration.GetConnectionString("AzureBlobStorage"));
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
                .AddEntityFrameworkStores<CVContext>();

            //Context and Repositories
            services.AddDbContext<CVContext>(options => options.UseSqlServer(configuration.GetConnectionString("AzureDB")));
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Services
            services.AddTransient<IIdentityService, IdentityService>();

            //Pagination
            services.Configure<PaginationOptions>(configuration.GetSection("Pagination"));

            //Loop Reference Handler
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                //options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            //Automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
