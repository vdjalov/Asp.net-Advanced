using CinemaWebAppOriginal.Services.Data.Interfaces;
using CinemaWebAppOriginal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebAppOriginal.Controllers
{
    [Authorize]
    public class CinemaController : Controller
    {

        private readonly ICinemaService cinemaService;
        
        public CinemaController(ICinemaService _cinemaService)
        {
            this.cinemaService = _cinemaService;
        }

        // List all cinemas
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllCinemaViewModel> cinemaIndexViewModels = await this.cinemaService.GetAllOrderedByLocationAsync();
                                
            return View(cinemaIndexViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CinemaCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CinemaCreateViewModel model)
        {
            if(!ModelState.IsValid)
            { 
                return View(model);
            }

            await this.cinemaService.CreateCinemaAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
           
            CinemaDetailsViewModel model = await this.cinemaService.GetDetailsByIdAsync(id);

            if(model == null)
            {
                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            IEnumerable<AllCinemaViewModel> cinemaIndexViewModels = 
                    await this.cinemaService.GetAllOrderedByLocationAsync();

            return View(cinemaIndexViewModels);
        } 

    }
}
