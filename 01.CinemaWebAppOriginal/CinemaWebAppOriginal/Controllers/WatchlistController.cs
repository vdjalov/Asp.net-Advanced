using CinemaWebAppOriginal.Data;
using CinemaWebAppOriginal.Data.Models;
using CinemaWebAppOriginal.Services.Data.Interfaces;
using CinemaWebAppOriginal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebAppOriginal.Controllers
{
    [Authorize]
    public class WatchlistController : Controller
    {
        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWatchlistService watchlistService;

        public WatchlistController(AppDbContext _context, UserManager<ApplicationUser> _userManager, IWatchlistService _watchlistService)
        {
            this.context = _context;
            this.userManager = _userManager;
            this.watchlistService = _watchlistService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = this.userManager.GetUserId(User);

            ICollection<WatchlistViewModel> watchlistMovies = 
                await this.watchlistService.GetAllWatchlistMoviesForUserAsync(userId);
                
            return View(watchlistMovies);
        }


        [HttpPost]
        public async Task<IActionResult> AddToWatchlist(int movieId)
        {

            string userId = this.userManager.GetUserId(User);

            UserMovie checkIfAlreadyExists = await context.UsersMovies.FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);

            if (checkIfAlreadyExists != null)
            {
                return RedirectToAction("Index", "Movie");
            }

            UserMovie newUserMovie = new UserMovie()
            {
                UserId = userId,
                MovieId = movieId,
            };
           
            await context.UsersMovies.AddAsync(newUserMovie);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), "Movie");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromWatchlist(int movieId)
        {
            string userId = this.userManager.GetUserId(User);

            UserMovie checkIfAlreadyExists = await context.UsersMovies.FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);

            if (checkIfAlreadyExists == null)
            {
                return RedirectToAction(nameof(Index));
            }

            this.context.UsersMovies.Remove(checkIfAlreadyExists);
            await this.context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }
    }
}
