
using CinemaWebAppOriginal.Data;
using CinemaWebAppOriginal.Data.Models;
using CinemaWebAppOriginal.Infrastructure.Repositories;
using CinemaWebAppOriginal.Infrastructure.Repositories.Contracts;
using CinemaWebAppOriginal.Services.Data;
using CinemaWebAppOriginal.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CInemaWebAppOriginal.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("SQLServer");

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;              // prevents client-side script access
                options.Cookie.SameSite = SameSiteMode.None;       // allows cross-site cookies
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // must use HTTPS
            });

            builder.Services.AddCors(cfg =>
            {
                cfg.AddPolicy("AllowWebApp", policy =>
                {
                    policy.WithOrigins("http://localhost:7289")
                          //.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowCredentials()
                          .AllowAnyHeader();
                });
            });

           

            builder.Services.AddScoped<IRepository<Movie, int>, BaseRepository<Movie, int>>();
            builder.Services.AddScoped<IRepository<Ticket, int>, BaseRepository<Ticket, int>>();
            builder.Services.AddScoped<IRepository<Cinema, int>, BaseRepository<Cinema, int>>();
            builder.Services.AddScoped<IRepository<CinemaMovie, object>, BaseRepository<CinemaMovie, object>>();
            builder.Services.AddScoped<IRepository<UserMovie, object>, BaseRepository<UserMovie, object>>();
            builder.Services.AddScoped<IRepository<Manager, Guid>, BaseRepository<Manager, Guid>>();

            builder.Services.AddScoped<ICinemaService, CinemaService>();
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<IWatchlistService, WatchlistService>();
            builder.Services.AddScoped<IManagerService, ManagerService>();
            builder.Services.AddScoped<ITicketService, TicketService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "ProductsAPI v1");
                });
            }
            app.UseRouting();

            app.UseCors("AllowWebApp");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
