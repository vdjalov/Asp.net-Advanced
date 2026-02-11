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

        // Creating a cinema and saving it to the DB
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
                                                .Where(c => !c.IsDeleted)
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

        // Snatching the cinema by id and returning it to Details View
        public async Task<CinemaDetailsViewModel> GetCinemaDetailsByIdAsync(int id)
        {
            Cinema ?cinema = await this.cinemaRepository.GetAllAttached()
                                         .Where(c => !c.IsDeleted)
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

        // Snatching the cinema by id and returning it to Edit View
        public async Task<CinemaEditViewModel> EditCinemaByIdAsync(int id)
        {
           CinemaEditViewModel ?model = await this.cinemaRepository.GetAllAttached()
                                         .Where(c => c.Id == id && c.IsDeleted == false)
                                         .Select(c => new CinemaEditViewModel
                                         {
                                             Id = c.Id,
                                             Name = c.Name,
                                             Location = c.Location,
                                         })
                                         .FirstOrDefaultAsync();

            return model;
        }

        // Snatching the cinema by id, editing it and saving the changes to the DB
        public async Task EditPostCinemaByIdAsync(CinemaEditViewModel model)
        {
            Cinema ?cinema = await this.cinemaRepository.GetAllAttached()
                                         .FirstOrDefaultAsync(c => c.Id == model.Id);
            // check anyway
            if (cinema == null)
            {
                throw new ArgumentException("Cinema with the provided id does not exist.");
            }

            cinema.Name = model.Name;
            cinema.Location = model.Location;

            await this.cinemaRepository.UpdateAndSaveAsync(cinema);
        }


        // checking if the cinema exists in the DB by id    
        public async Task<bool> CheckIfCinemaExists(int id)
        {
            return await this.cinemaRepository.GetAllAttached()
                                         .AnyAsync(c => c.Id == id);    
        }

        // soft deleting the cinema by id and saving the changes to the DB
        public async Task<bool> SoftDeleteCinemaAsync(int id)
        {
            Cinema ?cinema = await this.cinemaRepository.GetAllAttached()
                                         .Include(c => c.CinemaMovies)
                                         .FirstOrDefaultAsync(c => c.Id == id);

            if(cinema == null) // check if the cinema exists in the DB by id
            {
                return false;
            }

            bool hasActiveMovies = cinema.CinemaMovies.Any(cm => cm.CinemaId == id);

            if(hasActiveMovies) // check if the cinema has active movies
            {
                return false;
            }

            cinema.IsDeleted = true; // soft delete the cinema

            await this.cinemaRepository.UpdateAndSaveAsync(cinema); // saving the changes to the DB

            return true;
        }

        
    }
}
