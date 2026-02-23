using CinemaWebAppOriginal.Data;
using CinemaWebAppOriginal.Data.Models;
using CinemaWebAppOriginal.Infrastructure.Repositories;
using CinemaWebAppOriginal.Infrastructure.Repositories.Contracts;
using CinemaWebAppOriginal.Services.Data;
using CinemaWebAppOriginal.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationDatabase(this IServiceCollection services, IConfiguration config)
        {
            string connectionString = config.GetConnectionString("SQLServer") ?? throw new InvalidOperationException("Connection string does not exist");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            return services;
        }

        public static IServiceCollection AddApplicationIdentity(this IServiceCollection services, IConfiguration config)
        {
            services.AddDefaultIdentity<ApplicationUser>(options =>
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


            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IRepository<Movie, int>, BaseRepository<Movie, int>>();
            services.AddScoped<IRepository<Ticket, int>, BaseRepository<Ticket, int>>();
            services.AddScoped<IRepository<Cinema, int>, BaseRepository<Cinema, int>>();
            services.AddScoped<IRepository<CinemaMovie, object>, BaseRepository<CinemaMovie, object>>();
            services.AddScoped<IRepository<UserMovie, object>, BaseRepository<UserMovie, object>>();
            services.AddScoped<IRepository<Manager, Guid>, BaseRepository<Manager, Guid>>();

            services.AddScoped<ICinemaService, CinemaService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IWatchlistService, WatchlistService>();
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<ITicketService, TicketService>();


            return services;
        }
    }
}
