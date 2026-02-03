using CinemaWebAppOriginal.Data.Models;
using CinemaWebAppOriginal.Infrastructure.Repositories.Contracts;
using CinemaWebAppOriginal.Services.Data.Interfaces;
using CinemaWebAppOriginal.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaWebAppOriginal.Services.Data
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie, int> movieRepository;

        public MovieService(IRepository<Movie, int> _movieRepository)
        {
            this.movieRepository = _movieRepository;
        }


        public async Task<AddMovieToCinemaProgramViewModel> AddMovieToCinemaProgramGetView(int movieId)
        {
            AddMovieToCinemaProgramViewModel ?viewModel = this.movieRepository.GetAllAttached()
                .Where(m => m.Id == movieId)
                .Select(m => new AddMovieToCinemaProgramViewModel
                {
                    MovieId = m.Id,
                    MovieTitle = m.Title,
                    Cinemas = this.movieRepository.GetAllAttached()
                    .SelectMany(cm => cm.CinemaMovies)
                    .Select(c => c.Cinema)
                    .Distinct()
                        .Select(cb => new CinemaCheckBoxItem
                        {
                            Id = cb.Id,
                            Name = cb.Name,
                            IsSelected = this.movieRepository.GetAllAttached()
                                .Where(m => m.Id == movieId)
                                .SelectMany(m => m.CinemaMovies)
                                .Any(cm => cm.CinemaId == cb.Id)
                        }).ToList(),
                }).FirstOrDefault();

            return viewModel;
        }

        public async Task AddMovieToACinemaProgram(AddMovieToCinemaProgramViewModel model)
        {
            List<CinemaMovie> existingAssignments = await movieRepository.GetAllAttached()
                                         .SelectMany(cm => cm.CinemaMovies)
                                         .Where(cm => cm.MovieId == model.MovieId)
                                         .ToListAsync();
            
            Movie movie = await this.movieRepository.GetByIdAsync(model.MovieId);

            //this.movieRepository.DeleteRange(existingAssignments);

            foreach (var cinema in model.Cinemas)
            {
                if (cinema.IsSelected && !existingAssignments.Any(c => c.CinemaId == cinema.Id))
                {

                    var cinemaMovie = new CinemaMovie
                    {
                        CinemaId = cinema.Id,
                        MovieId = model.MovieId,
                    };
                   
                    movie.CinemaMovies.Add(cinemaMovie);
                }
            }
            await this.movieRepository.UpdateAsync(movie);

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
