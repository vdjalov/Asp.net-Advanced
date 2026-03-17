using CinemaWebAppOriginal.Data.Configurations.Data;
using CinemaWebAppOriginal.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
        public static async Task ImportMoviesAsync(IServiceProvider serviceProvider, string jsonFilePath) 
        {
            
            AppDbContext context = serviceProvider.GetRequiredService<AppDbContext>();
            List<Movie> moviesInDb = await context.Movies.ToListAsync();

            //try
            //{
                string jsonData = await File.ReadAllTextAsync(jsonFilePath);
                ImportMoviesDto[] movies = JsonSerializer.Deserialize<ImportMoviesDto[]>(jsonData);

                if(movies == null || movies.Count() == 0) 
                {
                    throw new Exception("No movies found in the JSON file.");
                }

               foreach (var movieDto in movies)
                {
                    if(IsValid(movieDto) == false)
                    {
                       continue;
                    }

                    bool isReleaseDateValid = DateTime.TryParse(movieDto.ReleaseDate.ToString(), out DateTime releaseDate);

                    if(!isReleaseDateValid) // check if release date is valid, if not skip the movie
                    {
                        continue;
                    }

                    if(moviesInDb.Any(m => m.Title == movieDto.Title && m.ReleaseDate == releaseDate)) // check if movie already exists in the database, if yes skip it
                    {
                        continue;
                    }

                    Movie movie = new Movie
                    {
                        Title = movieDto.Title,
                        Genre = movieDto.Genre,
                        ReleaseDate = releaseDate,
                        Duration = movieDto.Duration,
                        Director = movieDto.Director,
                        Description = movieDto.Description,
                        ImageUrl = movieDto.ImageUrl
                    };

                    await context.Movies.AddAsync(movie);
                }

                await context.SaveChangesAsync();
            }
            //catch (Exception ex)
            //{
            //    throw new Exception($"Failed to import movies: {ex.Message}");
            //}
        //}


        private static bool IsValid(object obj) // check if model is valid based on data annotations
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();    
            var context = new ValidationContext(obj);
            var isValid = Validator.TryValidateObject(obj, context, validationResults);

            return isValid;
        }

    }
}
