using CinemaWebAppOriginal.Services.Data.Interfaces;
using CinemaWebAppOriginal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebAppOriginal.Controllers
{
 
    public class CinemaController : BaseController
    {

        private readonly ICinemaService cinemaService;
        private readonly IManagerService managerService;
        public CinemaController(ICinemaService _cinemaService, IManagerService _managerService)
        {
            this.cinemaService = _cinemaService;
            this.managerService = _managerService;
        }

        // List all cinemas
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            ViewBag.userId = this.GetUserId();
            ViewBag.isUserManager = await this.managerService.IsUserAManager(ViewBag.userId);

            IEnumerable<AllCinemaViewModel> cinemaIndexViewModels = await this.cinemaService.GetAllOrderedByLocationAsync();
                                
            return View(cinemaIndexViewModels);
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

            return View(new CinemaCreateViewModel());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CinemaCreateViewModel model)
        {
            string userId = this.GetUserId();
            bool isUserManager = await this.managerService.IsUserAManager(userId);

            if(!isUserManager)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            string userId = this.GetUserId();
            bool isUserManager = await this.managerService.IsUserAManager(userId);

            if(!isUserManager)
            {
                return RedirectToAction(nameof(Index));
            }

            IEnumerable<AllCinemaViewModel> cinemaIndexViewModels = 
                    await this.cinemaService.GetAllOrderedByLocationAsync();

            return View(cinemaIndexViewModels);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string userId = this.GetUserId();
            bool isUserManager = await this.managerService.IsUserAManager(userId);
            if (!isUserManager)
            {
                return RedirectToAction(nameof(Manage));
            }

            CinemaEditViewModel model = await this.cinemaService.EditCinemaByIdAsync(id);

            if(model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(CinemaEditViewModel model)
        {
            string userId = this.GetUserId();
            bool isUserManager = await this.managerService.IsUserAManager(userId);

            if (!isUserManager)
            {
                return RedirectToAction(nameof(Manage));
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

           bool doesCinemaExist =  await this.cinemaService.CheckIfCinemaExists(model.Id);

            if(doesCinemaExist == false)
            {
                return View(model);
            }

            await this.cinemaService.EditPostCinemaByIdAsync(model);

            return RedirectToAction(nameof(Manage));
        }




    }

}
