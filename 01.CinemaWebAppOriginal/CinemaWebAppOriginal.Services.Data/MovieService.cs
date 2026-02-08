using CinemaWebAppOriginal.Data.Models;
using CinemaWebAppOriginal.Infrastructure.Repositories.Contracts;
using CinemaWebAppOriginal.Services.Data.Interfaces;
using CinemaWebAppOriginal.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebAppOriginal.Services.Data
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie, int> movieRepository;
        private readonly IRepository<Cinema, int> cinemaRepository;
        private readonly IRepository<CinemaMovie, object> cinemaMovieRepository;


        public MovieService(IRepository<Movie, int> _movieRepository, 
            IRepository<Cinema, int> _cinemaRepository, 
            IRepository<CinemaMovie, object> _cinemaMovieRepository)
        {
            this.movieRepository = _movieRepository;
            this.cinemaRepository = _cinemaRepository;
            this.cinemaMovieRepository = _cinemaMovieRepository;
        }


        public async Task<AddMovieToCinemaProgramViewModel> AddMovieToCinemaProgramGetView(int movieId)
        {
            Movie movie = await this.movieRepository.GetByIdAsync(movieId);

            AddMovieToCinemaProgramViewModel? viewModel = new AddMovieToCinemaProgramViewModel
            {
                MovieId = movieId,
                MovieTitle = movie.Title,
                Cinemas = this.cinemaRepository.GetAllAttached()
                        .Select(cb => new CinemaCheckBoxItem
                        {
                            Id = cb.Id,
                            Name = cb.Name,
                            IsSelected = this.movieRepository.GetAllAttached()
                                .Where(m => m.Id == movieId)
                                .SelectMany(m => m.CinemaMovies)
                                .Any(cm => cm.CinemaId == cb.Id)
                        }).ToList()
            };

            return viewModel;
        }

        public async Task AddMovieToACinemaProgramAsync(AddMovieToCinemaProgramViewModel model)
        {
            List<CinemaMovie> existingAssignments = await this.cinemaMovieRepository.GetAllAttached()
                                                            .Where(cm => cm.MovieId == model.MovieId)
                                                            .ToListAsync();

           await this.cinemaMovieRepository.DeleteRangeAndSaveChangesAsync(existingAssignments);
           

            foreach (var cinema in model.Cinemas)
            {
                if (cinema.IsSelected)
                {
                    var cinemaMovie = new CinemaMovie
                    {
                        CinemaId = cinema.Id,
                        MovieId = model.MovieId,
                    };
                   
                  await cinemaMovieRepository.AddAndSaveAsync(cinemaMovie);
                }
            }
           
        }

        public async Task<bool> CheckIfMovieExists(int movieId)
        {
           Movie movie =  await this.movieRepository.GetByIdAsync(movieId);

            if(movie == null)
            {
                return false;
            }
                return true;
        }

        public async Task CreateMovieAsync(MovieViewModel viewModel)
        {
            Movie movie = new Movie()
            {
                Title = viewModel.Title,
                Genre = viewModel.Genre,
                ReleaseDate = viewModel.ReleaseDate,
                Director = viewModel.Director,
                Duration = viewModel.Duration,
                Description = viewModel.Description,
                ImageUrl = viewModel.ImageUrl,
            };

            await this.movieRepository.AddAndSaveAsync(movie);
        }

        public async Task<ICollection<AllMoviesViewModel>> GetAllMoviesAsync()
        {

            ICollection<AllMoviesViewModel> moviesInDb = await this.movieRepository.GetAllAttached()
                    .Select(m => new AllMoviesViewModel
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Genre = m.Genre,
                        ReleaseDate = m.ReleaseDate,
                        Director = m.Director,
                        Duration = m.Duration,
                    }).ToListAsync();

            return moviesInDb;
        }

        public async Task<MovieViewModel> GetMovieDetailsById(int id)
        {
            MovieViewModel ?movie = await this.movieRepository.GetAllAttached()
                .Where(m => m.Id == id)
                .Select(m => new MovieViewModel
                {
                    Title = m.Title,
                    Genre = m.Genre,
                    ReleaseDate = m.ReleaseDate,
                    Director = m.Director,
                    Duration = m.Duration,
                    Description = m.Description,
                    ImageUrl = m.ImageUrl,
                }).FirstOrDefaultAsync();

            return movie;
            
        }
    }
}
