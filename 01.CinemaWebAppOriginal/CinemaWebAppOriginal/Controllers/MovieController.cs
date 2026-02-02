using Microsoft.AspNetCore.Mvc;

using CinemaWebAppOriginal.Data;
using CinemaWebAppOriginal.Data.Models;
using CinemaWebAppOriginal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using CinemaWebAppOriginal.Services.Data.Interfaces;

namespace CinemaWebAppOriginal.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        private readonly AppDbContext context;
        private readonly IMovieService movieService;


        public MovieController(AppDbContext _context, IMovieService _movieService)
        {
            this.context = _context;
            this.movieService = _movieService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ICollection<AllMoviesViewModel> movies = await this.movieService.GetAllMoviesAsync();

            return View(movies);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View(new MovieViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Create(MovieViewModel viewModel)
        {
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


        [HttpGet]
        public async Task <IActionResult> AddToProgram(int movieId)
        {
            Movie? movie = await this.context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
            {
                return RedirectToAction(nameof(Index));
            }

            List<Cinema> cinemas = await this.context.Cinemas.ToListAsync();

            AddMovieToCinemaProgramViewModel viewModel = new AddMovieToCinemaProgramViewModel()
            {
                MovieId = movieId,
                MovieTitle = movie.Title,
                Cinemas = cinemas.Select(c => new CinemaCheckBoxItem
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsSelected = false
                }).ToList(),
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> AddToProgram(AddMovieToCinemaProgramViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            List<CinemaMovie> existingAssignment = await this.context.CinemasMovies
                                         .Where(cm => cm.MovieId == model.MovieId)
                                         .ToListAsync();

            this.context.RemoveRange(existingAssignment);

            foreach (var cinema in model.Cinemas)
            {
                if (cinema.IsSelected)
                {
                    var cinemaMovie = new CinemaMovie
                    {
                        CinemaId = cinema.Id,
                        MovieId = model.MovieId,
                    };
                    await context.CinemasMovies.AddAsync(cinemaMovie);
                }
            }
            await this.context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
