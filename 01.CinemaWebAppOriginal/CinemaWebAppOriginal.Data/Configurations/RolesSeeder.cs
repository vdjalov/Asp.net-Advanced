using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaWebAppOriginal.Data.Configurations
{
    public static class RolesSeeder
    {
        public static void SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            string[] roleNames = { "Admin", "User", "Manager" };

            foreach (var roleName in roleNames)
            {
                var roleExists = roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult();
                if (!roleExists)
                {
                    var result = roleManager.CreateAsync(new IdentityRole<Guid> {Name = roleName }).GetAwaiter().GetResult();
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Failed to create role: {roleName}");
                    }
                }
            }
        }
    }
}
