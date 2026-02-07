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
        

        public WatchlistService(IRepository<UserMovie, object> _userMovieRepository)
        {
            this.userMovieRepository = _userMovieRepository;
        }
      

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
    }
}
