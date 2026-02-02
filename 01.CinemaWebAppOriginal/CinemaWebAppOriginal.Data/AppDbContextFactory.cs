using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;



namespace CinemaWebAppOriginal.Data
{
    //public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    //{
    //    AppDbContext IDesignTimeDbContextFactory<AppDbContext>.CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

    //        IConfigurationRoot configuration = new ConfigurationBuilder()
    //            .SetBasePath(Directory.GetCurrentDirectory())
    //            .AddJsonFile("appsettings.json")
    //            .Build();

    //        string connectionString = configuration.GetConnectionString("SQLServer");
    //        optionsBuilder.UseSqlServer(connectionString);

    //        return new AppDbContext(optionsBuilder.Options);
    //    }
    //}
}
