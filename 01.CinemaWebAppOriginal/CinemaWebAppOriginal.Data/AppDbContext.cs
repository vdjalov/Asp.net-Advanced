using CinemaWebAppOriginal.Data.Configurations;
using CinemaWebAppOriginal.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebAppOriginal.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)   {}

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet <CinemaMovie> CinemasMovies { get; set; }
    public DbSet<UserMovie> UsersMovies { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Manager> Managers { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CinemaMovieConfiguration());
        modelBuilder.ApplyConfiguration(new MovieConfiguration());

        base.OnModelCreating(modelBuilder);

       

                    
    }


}
