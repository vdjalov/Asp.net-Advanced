using CinemaWebAppOriginal.Data;
using CinemaWebAppOriginal.Services.Data.Interfaces;
using CinemaWebAppOriginal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CinemaWebAppOriginal.Controllers
{
    [Authorize]
    public class WatchlistController : Controller
    {
        private readonly IWatchlistService watchlistService;

        public WatchlistController( IWatchlistService _watchlistService)
        {
            this.watchlistService = _watchlistService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = this.GetUserId();

            ICollection<WatchlistViewModel> watchlistMovies = 
                await this.watchlistService.GetAllWatchlistMoviesForUserAsync(userId);
                
            return View(watchlistMovies);
        }


        // Adding a movie to the user's watchlist 
        [HttpPost]
        public async Task<IActionResult> AddToWatchlist(int movieId)
        {
            string userId = this.GetUserId();

            bool checkIfAlreadyExists = await this.watchlistService.CheckIfMovieAlreadyAddedInWatchlistAync(movieId, userId);

            if (checkIfAlreadyExists)
            {
                return RedirectToAction("Index", "Movie");
            }

            await this.watchlistService.AddMovieToUserWatchlistAsync(movieId, userId);

            return RedirectToAction(nameof(Index), "Movie");
        }


        //removing a movie from the user's watchlist
        [HttpPost]
        public async Task<IActionResult> RemoveFromWatchlist(int movieId)
        {
            string userId = this.GetUserId();

            bool checkIfAlreadyExists = await this.watchlistService.CheckIfMovieAlreadyAddedInWatchlistAync(movieId, userId);

            if (!checkIfAlreadyExists)
            {
                return RedirectToAction(nameof(Index));
            }

            await this.watchlistService.RemoveMovieFromUserWatchlistAsync(movieId, userId);
           
            return RedirectToAction(nameof(Index));
        }



        // Helper method to get the current user's ID
        private string GetUserId()
         => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
