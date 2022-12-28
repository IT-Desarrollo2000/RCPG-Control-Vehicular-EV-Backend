using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
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

            var adminUser = await userManager.FindByNameAsync("CVAdmin");
            await userManager.AddToRoleAsync(adminUser, "Administrator");
        }
    }
}
