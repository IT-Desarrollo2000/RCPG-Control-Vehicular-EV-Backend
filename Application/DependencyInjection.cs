using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            ///Registrar servicios
            ///
            services.AddScoped<ITokenService, TokenService>();
            services.AddTransient<IProfileServices, ProfileServices>();
            services.AddTransient<IUserApprovalServices, UserApprovalServices>();
            services.AddTransient<ICompanyServices, CompanyServices>();
            services.AddTransient<IDepartamentServices, DepartamentServices>();
            services.AddTransient<IVehicleServiService, VehicleServiServices>();
            services.AddTransient<IChecklistServices, ChecklistServices>();
            services.AddTransient<IExpensesServices, ExpensesServices>();
            services.AddTransient<ITypeOfExpensesServices, TypeOfExpensesServices>();
            services.AddTransient<IVehicleMaintenanceService, VehicleMaintenanceServices>();
            services.AddTransient<IMaintenanceWorkshopService, MaintenanceWorkshopServices>();
            services.AddTransient<IRegisteredVehiclesServices, RegisteredVehiclesServices>();
            services.AddTransient<IVehicleReportService, VehicleReportServices>();
            services.AddTransient<IDestinationOfReportUseService, DestinationOfReportUseServices>();
            services.AddTransient<IVehicleReportUseService, VehicleReportUseServices>();
            services.AddTransient<IPolicyService, PolicyServices>();
            services.AddTransient<IToolsServices, ToolsServices>();

            //Parametros para validación de tokens
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokenkey"])),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                RequireExpirationTime = true,
                RequireSignedTokens= true
                //Si el token es menor a 5 Min
                //, ClockSkew = TimeSpan.Zero
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = tokenValidationParameters;
                    
                });

            return services;
        }
    }
}
