using CinemaWebAppOriginal.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace CinemaWebAppOriginal.Data.Configurations
{
    public static class Seeder
    {

        // import roles available in the system
        public static void SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            string[] roleNames = { "Admin", "User", "Manager" };

            foreach (var roleName in roleNames)
            {
                var roleExists = roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult();
                if (!roleExists)
                {
                    IdentityResult result = roleManager.CreateAsync(new IdentityRole<Guid> {Name = roleName }).GetAwaiter().GetResult();
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Failed to create role: {roleName}");
                    }
                }
            }
        }


        // import admin user and assign admin role
        public static void  AssignAdminRole(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string adminEmail = "admin@example.com";
            string adminPassword = "Admin@123";

            var adminUser = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail
                };

                var createUserResult = userManager.CreateAsync(adminUser, adminPassword).GetAwaiter().GetResult();

                if (!createUserResult.Succeeded)
                {
                    throw new Exception($"Failed to create admin user: {adminEmail}");
                }
            }

            bool isInRole = userManager.IsInRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();

            if (!isInRole)
            {
                var addRoleResult = userManager.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
                if (!addRoleResult.Succeeded)
                {
                    throw new Exception($"Failed to assign admin role to user: {adminEmail}");
                }
            }
        }

        //import movies from json file
        public static async Task ImportMovies(IServiceProvider serviceProvider, string jsonFilePath) 
        {
            await using AppDbContext context = serviceProvider.GetRequiredService<AppDbContext>();

            if(context.Movies.Any())
            {
                return; // Movies already exist, no need to import
            }

            try
            {
                string jsonData = await File.ReadAllTextAsync(jsonFilePath);
                var movies = JsonSerializer.Deserialize<List<Movie>>(jsonData);

                if(movies == null || movies.Count == 0) 
                {
                    throw new Exception("No movies found in the JSON file.");
                }

                await context.Movies.AddRangeAsync(movies);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to import movies: {ex.Message}");
            }
        }


    }
}
