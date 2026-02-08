using CinemaWebAppOriginal.Data;
using CinemaWebAppOriginal.Services.Data.Interfaces;
using CinemaWebAppOriginal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CinemaWebAppOriginal.Controllers
{
    
    public class MovieController : Controller
    {
        
        private readonly IMovieService movieService;
        private readonly IManagerService managerService;

        public MovieController(IMovieService _movieService, IManagerService _managerService)
        {
            this.movieService = _movieService;
            this.managerService = _managerService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ICollection<AllMoviesViewModel> movies = await this.movieService.GetAllMoviesAsync();

            return View(movies);
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            string userId = this.GetUserId();
            bool isUserManager = await this.managerService.IsUserAManager(userId);

            if (!isUserManager)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(new MovieViewModel());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(MovieViewModel viewModel)
        {
            string userId = this.GetUserId();
            bool isUserManager = await this.managerService.IsUserAManager(userId);

            if (!isUserManager)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
              await this.movieService.CreateMovieAsync(viewModel);

              return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            bool doesMovieExist = await this.movieService.CheckIfMovieExists(id);

            if (!doesMovieExist)
            {
                return RedirectToAction(nameof(Index));
            }

            MovieViewModel movie = await this.movieService.GetMovieDetailsById(id);

            return View(movie);
        }

        [Authorize]
        [HttpGet]
        public async Task <IActionResult> AddToProgram(int movieId)
        {
            string userId = this.GetUserId();
            bool isUserManager = await this.managerService.IsUserAManager(userId);

            if (!isUserManager)
            {
                return RedirectToAction(nameof(Index));
            }

            bool checkIfMovieExists = await this.movieService.CheckIfMovieExists(movieId);  

            if (!checkIfMovieExists)
            {
                ModelState.AddModelError(string.Empty, "Movie does not exist.");
                return RedirectToAction(nameof(Index));
            }

            AddMovieToCinemaProgramViewModel viewModel = await this.movieService.AddMovieToCinemaProgramGetView(movieId);

            return View(viewModel);
        }
       
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToProgram(AddMovieToCinemaProgramViewModel model)
        {
            string userId = this.GetUserId();
            bool isUserManager = await this.managerService.IsUserAManager(userId);

            if (!isUserManager)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this.movieService.AddMovieToACinemaProgramAsync(model);

            return RedirectToAction(nameof(Index));
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            string userId = this.GetUserId();
            bool isUserManager = await this.managerService.IsUserAManager(userId);

            if (!isUserManager)
            {
                return RedirectToAction(nameof(Index));
            }

            ICollection<AllMoviesViewModel> movies = await this.movieService.GetAllMoviesAsync();

            return View(movies);
        }

        // Method to get the user id from the claims
        private string GetUserId()
            => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
