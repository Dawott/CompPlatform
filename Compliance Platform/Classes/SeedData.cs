using Compliance_Platform.Model;
using Microsoft.AspNetCore.Identity;

namespace Compliance_Platform.Classes
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<CompPlatformUser>>();

                // Tworzenie ról (jeśli nie istnieją)
                string[] roleNames = { "Administrator", "Audytor", "Rejestrator" };
                foreach (var roleName in roleNames)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                // Default admin
                var adminEmail = "admin@example.com";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);

                if (adminUser == null)
                {
                    adminUser = new CompPlatformUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        Imie = "Admin",
                        Nazwisko = "User",
                        Departament = "IT",
                        Rola = "Administrator"
                    };

                    var result = await userManager.CreateAsync(adminUser, "Admin123!");

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Administrator");
                    }
                }
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Wystąpił błąd podczas dostępu do bazy.");
            }
        }
    }
}
