using CinemaWebAppOriginal.Services.Data.Interfaces;
using CinemaWebAppOriginal.ViewModels;
using CinemaWebAppOriginal.ViewModels.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebAppOriginal.Controllers
{
    public class TicketController : BaseController
    {

        private readonly ITicketService ticketService;
        private readonly ICinemaService cinemaService;
        private readonly IManagerService managerService;
        public TicketController(ITicketService _ticketService, ICinemaService _cinemaService, IManagerService _managerService)
        {
            this.ticketService = _ticketService;
            this.cinemaService = _cinemaService;
            this.managerService = _managerService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> BuyTicket(int cinemaId, int movieId)
        {
            bool isManager = await this.managerService.IsUserAManager(this.GetUserId());
            if (isManager)
            {
                TempData["ErrorMessage"] = "Managers can not buy tickets.";
                return RedirectToAction(nameof(Index), "Home");
            }

            BuyTicketViewModel viewModel = new BuyTicketViewModel
            {
                CinemaId = cinemaId,
                MovieId = movieId,
                UserId = this.GetUserId(),
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> BuyTicket(BuyTicketViewModel viewModel)
        {
            bool isManager = await this.managerService.IsUserAManager(this.GetUserId());
            if (isManager)
            {
                TempData["ErrorMessage"] = "Managers can not buy tickets.";
                return RedirectToAction(nameof(Index), "Home");
            }

            if (ModelState.IsValid)
            {
                TempData["SuccessMessage"] = "Ticket bought successfully!";
                return RedirectToAction(nameof(Index), "Home");
            }

            bool success = await this.ticketService.BuyTicketAsync(viewModel.CinemaId, viewModel.MovieId, viewModel.UserId);
            if (!success)
            {
                TempData["ErrorMessage"] = "Failed to buy ticket. Please try again.";
                return View(viewModel);
            }

            return RedirectToAction(nameof(MyTickets));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyTickets()
        {
            Guid userId = Guid.Parse(this.GetUserId());

            if (userId == Guid.Empty)
            {
                TempData["ErrorMessage"] = "Invalid user ID.";
                return RedirectToAction(nameof(Index), "Home");
            }

            IEnumerable<UserTicketViewModel> tickets = await this.ticketService.GetUserTicketsAsync(userId);

            return View(tickets);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Manage()
        {
            bool isManager = await this.managerService.IsUserAManager(this.GetUserId());
            if (!isManager)
            {
                TempData["ErrorMessage"] = "Only managers can access this page.";
                return RedirectToAction(nameof(Index), "Home");
            }

            IEnumerable<AllCinemaViewModel> cinemas = await this.cinemaService.GetAllOrderedByLocationAsync();
            return View(cinemas);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SetAvailableTickets(int cinemaId, int movieId)
        {
            bool isManager = await this.managerService.IsUserAManager(this.GetUserId());
            if (!isManager)
            {
                TempData["ErrorMessage"] = "Only managers can access this page.";
                return RedirectToAction(nameof(Index), "Home");
            }

            SetAvailableTicketsViewModel viewModel = new SetAvailableTicketsViewModel
            {
                CinemaId = cinemaId,
                MovieId = movieId
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SetAvailableTickets(SetAvailableTicketsViewModel viewModel)
        {
            bool isManager = await this.managerService.IsUserAManager(this.GetUserId());
            if (!isManager)
            {
                TempData["ErrorMessage"] = "Only managers can access this page.";
                return RedirectToAction(nameof(Index), "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            bool success = await this.ticketService.SetAvailableTicketsAsync(viewModel);
            if (!success)
            {
                TempData["ErrorMessage"] = "Failed to set available tickets. Please try again.";
                return View(viewModel);
            }
            TempData["SuccessMessage"] = "Available tickets updated successfully!";

            return RedirectToAction(nameof(Manage));
        }

    }
}
