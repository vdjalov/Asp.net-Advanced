using CinemaWebAppOriginal.Data.Models;
using CinemaWebAppOriginal.Infrastructure.Repositories.Contracts;
using CinemaWebAppOriginal.Services.Data.Interfaces;
using CinemaWebAppOriginal.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebAppOriginal.Services.Data
{
    public class CinemaService : ICinemaService
    {
        private IRepository<Cinema, int> cinemaRepository;

        public CinemaService(IRepository<Cinema, int> _cinemaRepository)
        {
            this.cinemaRepository = _cinemaRepository;
        }

        public async Task CreateCinemaAsync(CinemaCreateViewModel model)
        {
            Cinema cinema = new Cinema()
            {
                Name = model.Name,
                Location = model.Location
            };

           await this.cinemaRepository.AddAndSaveAsync(cinema);
        }


        // Snatching all from DB and returning to Index View
        public async Task<IEnumerable<AllCinemaViewModel>> GetAllOrderedByLocationAsync()
        {

           IEnumerable<AllCinemaViewModel> cinemaIndexViewModels = await cinemaRepository
                                                .GetAllAttached()
                                                .Select(c => new AllCinemaViewModel
                                                {
                                                    Id = c.Id,
                                                    Name = c.Name,
                                                    Location = c.Location,
                                                })
                                                .OrderBy(c => c.Location)
                                                .ToArrayAsync();
                                                
            return cinemaIndexViewModels;
        }

        public async Task<CinemaDetailsViewModel> GetDetailsByIdAsync(int id)
        {
            Cinema ?cinema = await this.cinemaRepository.GetAllAttached()
                                         .Include(c => c.CinemaMovies)
                                         .ThenInclude(cm => cm.Movie)
                                         .FirstOrDefaultAsync(c => c.Id == id);

            CinemaDetailsViewModel ?model = null; 

            if (cinema != null)
            {
                     model = new CinemaDetailsViewModel()
                {
                    Id = cinema.Id,
                    Name = cinema.Name,
                    Location = cinema.Location,
                    Movies = cinema.CinemaMovies.Select(cm => new MovieProgramViewModel
                    {
                        Title = cm.Movie.Title,
                        Duration = cm.Movie.Duration,
                    }).ToList(),
                };

                return model;
            }

            return model;
        }

       
    }
}
