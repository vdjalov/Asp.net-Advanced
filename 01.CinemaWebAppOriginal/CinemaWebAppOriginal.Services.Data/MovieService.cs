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


        public Task AddMovieToACinemaProgram(int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task AddMovieToACinemaProgram(AddMovieToCinemaProgramViewModel model)
        {
            throw new NotImplementedException();
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

        public Task<IQueryable<MovieViewModel>> GetAllMoviesAttached()
        {
            throw new NotImplementedException();
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
