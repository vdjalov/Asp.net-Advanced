using Microsoft.EntityFrameworkCore;

using CinemaWebAppOriginal.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CinemaWebAppOriginal.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
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
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CinemaMovie>().HasKey(cm => new { cm.CinemaId, cm.MovieId});

                    
    }


}
