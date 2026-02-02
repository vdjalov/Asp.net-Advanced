using CinemaWebAppOriginal.Data;
using CinemaWebAppOriginal.Data.Models;
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

        public WatchlistController(AppDbContext _context, UserManager<ApplicationUser> _userManager)
        {
            this.context = _context;
            this.userManager = _userManager;
        }

        public async Task<IActionResult> Index()
        {
            string userId = this.userManager.GetUserId(User);

            ICollection<WatchlistViewModel> watchlistMovies = await context.UsersMovies
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
