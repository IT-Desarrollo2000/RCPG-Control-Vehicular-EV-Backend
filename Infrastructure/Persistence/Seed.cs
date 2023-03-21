using Application.Services;
using Domain.Entities.Identity;
using Domain.Entities.Registered_Cars;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, UnitOfWork unitOfWork)
        {
            //SI HAY USUARIOS ACTIVOS EL SEED NO CORRE, COMENTAR CREACION DE SUPER ADMIN PARA CORRER EL SEED CON ROLES EXTRA
            if (await userManager.Users.AnyAsync(x => x.UserName == "CGAdmin")) return;

            var user = new AppUser()
            {
                UserName = "CVAdmin",
                Email = "danicabhern@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var adminuser1 = await userManager.CreateAsync(user, "CVAdmin2023");

            if (!await roleManager.RoleExistsAsync("Administrator"))
            {
                await roleManager.CreateAsync(new AppRole { Name = "Administrator" });
            }

            if (!await roleManager.RoleExistsAsync("AdminUser"))
            {
                await roleManager.CreateAsync(new AppRole { Name = "AdminUser" });
            }

            if (!await roleManager.RoleExistsAsync("AppUser"))
            {
                await roleManager.CreateAsync(new AppRole { Name = "AppUser" });
            }

            if (!await roleManager.RoleExistsAsync("Supervisor"))
            {
                await roleManager.CreateAsync(new AppRole { Name = "Supervisor" });
            }

            var adminUser = await userManager.FindByNameAsync("CVAdmin");
            await userManager.AddToRoleAsync(adminUser, "Administrator");

            //Crear los tipos de gastos por default
            var fuelexists = await unitOfWork.TypesOfExpensesRepo.Get(e => e.Name == "Carga_Gasolina");
            if (fuelexists.SingleOrDefault() == null)
            {
                var newType = new TypesOfExpenses()
                {
                    Name = "Carga_Gasolina",
                    Description = "Gasto generado por Carga de Gasolina"
                };

                await unitOfWork.TypesOfExpensesRepo.Add(newType);
            }

            var correctiveMtn = await unitOfWork.TypesOfExpensesRepo.Get(e => e.Name == "Mantenimiento_Correctivo");
            if (correctiveMtn.SingleOrDefault() == null)
            {
                var newType = new TypesOfExpenses()
                {
                    Name = "Mantenimiento_Correctivo",
                    Description = "Gasto generado por Mantenimiento Correctivo"
                };

                await unitOfWork.TypesOfExpensesRepo.Add(newType);
            }

            var preventiveMtn = await unitOfWork.TypesOfExpensesRepo.Get(e => e.Name == "Mantenimiento_Preventivo");
            if (preventiveMtn.SingleOrDefault() == null)
            {
                var newType = new TypesOfExpenses()
                {
                    Name = "Mantenimiento_Preventivo",
                    Description = "Gasto generado por Mantenimiento Preventivo"
                };

                await unitOfWork.TypesOfExpensesRepo.Add(newType);
            }
        }
    }
}
