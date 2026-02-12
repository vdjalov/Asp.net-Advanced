using CinemaWebAppOriginal.Services.Data.Interfaces;
using CinemaWebAppOriginal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebAppOriginal.Controllers
{
    public class WatchlistController : BaseController
    {
        private readonly IWatchlistService watchlistService;

        public WatchlistController( IWatchlistService _watchlistService)
        {
            this.watchlistService = _watchlistService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = this.GetUserId();

            ICollection<WatchlistViewModel> watchlistMovies = 
                await this.watchlistService.GetAllWatchlistMoviesForUserAsync(userId);
                
            return View(watchlistMovies);
        }


        // Adding a movie to the user's watchlist 
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToWatchlist(int movieId)
        {
            string userId = this.GetUserId();

            bool checkIfAlreadyExists = await this.watchlistService.CheckIfMovieAlreadyAddedInWatchlistAync(movieId, userId);

            if (checkIfAlreadyExists)
            {
                TempData["ErrorMessage"] = "Movie has already been added to your watchlist.";
                return RedirectToAction("Index", "Movie");
            }

            await this.watchlistService.AddMovieToUserWatchlistAsync(movieId, userId);

            return RedirectToAction(nameof(Index), "Movie");
        }


        //removing a movie from the user's watchlist
        [Authorize]
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


    }
}
