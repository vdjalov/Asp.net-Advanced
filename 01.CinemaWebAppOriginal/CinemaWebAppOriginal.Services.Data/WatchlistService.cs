using CinemaWebAppOriginal.Data.Models;
using CinemaWebAppOriginal.Infrastructure.Repositories.Contracts;
using CinemaWebAppOriginal.Services.Data.Interfaces;
using CinemaWebAppOriginal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaWebAppOriginal.Services.Data
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IRepository<UserMovie, object> userMovieRepository;


        // Constructor injection of the UserMovie repository to access the watchlist data
        public WatchlistService(IRepository<UserMovie, object> _userMovieRepository)
        {
            this.userMovieRepository = _userMovieRepository;
        }


        // Getting all movies in the user's watchlist by querying the UserMovie repository with the userId and projecting the results into a collection of WatchlistViewModel
        public async Task<ICollection<WatchlistViewModel>> GetAllWatchlistMoviesForUserAsync(string userId)
        {

            ICollection<WatchlistViewModel> watchlistMovies = await userMovieRepository.GetAllAttached()
                                       .Where(um => um.UserId == userId)
                                       .Include(um => um.Movie)
                                       .Select(um => new WatchlistViewModel()
                                       {
                                           MovieId = um.MovieId,
                                           Title = um.Movie.Title,
                                           Genre = um.Movie.Genre,
                                           ReleaseDate = um.Movie.ReleaseDate.ToString("dd/MM/yyyy"),
                                           ImageUrl = um.Movie.ImageUrl,
                                       })
                                       .ToListAsync();

            return watchlistMovies;
        }

        //adding a movie to the user's watchlist by creating a new UserMovie entity with the userId and movieId and saving it to the UserMovie repository
        public async Task AddMovieToUserWatchlistAsync(int movieId, string userId)
        {
            UserMovie newUserMovie = new UserMovie()
            {
                UserId = userId,
                MovieId = movieId,
            };

             await userMovieRepository.AddAndSaveAsync(newUserMovie);
        }


        // Checking if a movie is already added in the user's watchlist by querying the UserMovie repository with the userId and movieId
        public async Task<bool> CheckIfMovieAlreadyAddedInWatchlistAync(int movieId, string userId)
        {
            return await this.userMovieRepository.GetAllAttached()
               .AnyAsync(um => um.UserId == userId && um.MovieId == movieId);
        }

        public async Task RemoveMovieFromUserWatchlistAsync(int movieId, string userId)
        {
           UserMovie userMovie = this.userMovieRepository.GetAllAttached().FirstOrDefault(um => um.UserId == userId && um.MovieId == movieId);
          
           await this.userMovieRepository.RemoveAsync(userMovie);
        }
    }
}
