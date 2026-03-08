using CinemaWebAppOriginal.Data.Configurations;

namespace CinemaWebAppOriginal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


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
                RolesSeeder.SeedRoles(services);    // Seed the roles into the database from RolesSeeder class in Configurations folder
                RolesSeeder.AssignAdminRole(services);
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
                
                if (context.User.Identity?.IsAuthenticated ==true && context.Request.Path == "/")
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
