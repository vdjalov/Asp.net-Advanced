using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


using CinemaWebAppOriginal.Data;
using CinemaWebAppOriginal.Data.Models;
using CinemaWebAppOriginal.Infrastructure.Repositories.Contracts;
using CinemaWebAppOriginal.Infrastructure.Repositories;
using CinemaWebAppOriginal.Services.Data.Interfaces;
using CinemaWebAppOriginal.Services.Data;


namespace CinemaWebAppOriginal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("SQLServer");

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            

            builder.Services.AddScoped<IRepository<Movie, int>, BaseRepository<Movie, int>>();
            builder.Services.AddScoped<IRepository<Ticket, int>, BaseRepository<Ticket, int>>();
            builder.Services.AddScoped<IRepository<Cinema, int>, BaseRepository<Cinema, int>>();
            builder.Services.AddScoped<IRepository<CinemaMovie, object>, BaseRepository<CinemaMovie, object>>();
            builder.Services.AddScoped<IRepository<UserMovie, object>, BaseRepository<UserMovie, object>>();

            builder.Services.AddScoped<ICinemaService, CinemaService>();
            builder.Services.AddScoped<IMovieService, MovieService>();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => 
            {
                //SignIn settings
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                // Password Settings
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

                //User Settings
                options.User.RequireUniqueEmail = true;

                //Lockout Settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            })
             .AddEntityFrameworkStores<AppDbContext>();

            var app = builder.Build();

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
            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
