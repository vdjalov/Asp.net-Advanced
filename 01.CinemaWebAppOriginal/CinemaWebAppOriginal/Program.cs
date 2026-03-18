using CinemaWebAppOriginal.Data;
using CinemaWebAppOriginal.Data.Configurations;

namespace CinemaWebAppOriginal
{
    public class Program
    {
        // D:\SoftUni\Github-repos\ASP.Net Advanced\Asp.net-advanced-exercises\01.CinemaWebAppOriginal\CinemaWebAppOriginal.Data\Configurations\Data
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string? jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "movies.json"); // fetching the path of movies.json file to import movies into the database

            builder.Services.AddRazorPages();   
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // Adding davtabase
            builder.Services.AddApplicationDatabase(builder.Configuration);
            // Adding services
            builder.Services.AddApplicationServices(builder.Configuration);
            // adding identity
            builder.Services.AddApplicationIdentity(builder.Configuration);
           

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                Seeder.SeedRoles(services);    // Seed the roles into the database from RolesSeeder class in Configurations folder
                Seeder.AssignAdminRole(services); // create roles and assign admin role to the user with email "

                if(jsonFilePath != null)
                {
                    await Seeder.ImportMoviesAsync(services, jsonFilePath); // import movies from movies.json file into the database
                }
                
            }

            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {

                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();

            app.Use((context, next) =>
            {
                
                if (context.User.Identity?.IsAuthenticated ==true && context.Request.Path == "/") // If the user is authenticated and trying to access the root URL
                {
                    if (context.User.IsInRole("Admin"))
                    {
                        context.Response.Redirect("/Admin/Home/Index");
                        return Task.CompletedTask;
                    }
                }
                return next();
            });

            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"); // Map the route for areas (Admin and User)

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

   
        }
    }
}
