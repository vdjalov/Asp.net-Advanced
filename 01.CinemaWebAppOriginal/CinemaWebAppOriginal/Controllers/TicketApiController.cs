using CinemaWebAppOriginal.Services.Data.Interfaces;
using CinemaWebAppOriginal.ViewModels;
using CinemaWebAppOriginal.ViewModels.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebAppOriginal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketApiController : BaseController
    {
        private readonly ITicketService ticketService;
        private readonly ICinemaService cinemaService;
        private readonly IManagerService managerService;

        public TicketApiController(ITicketService _ticketService, ICinemaService _cinemaService, IManagerService _managerService)
        {
            this.ticketService = _ticketService;
            this.cinemaService = _cinemaService;
            this.managerService = _managerService;
        }



        [HttpGet("GetMoviesByCinema/{cinemaId}")]
        public async Task<IActionResult> GetMoviesByCinema(int cinemaId)
        {
            string userId = this.GetUserId();
            bool isUserManager = await this.managerService.IsUserAManager(userId);
            if (!isUserManager)
            {
                return Unauthorized("Only Managers can access this endpoint.");
            }

            CinemaProgramViewModel movies = await this.cinemaService.GetCinemaProgramByIdAsync(cinemaId);

            return Ok(movies);
        }

        [HttpPost("UpdateAvailableTickets")]
        public async Task<IActionResult> UpdateAvailableTickets([FromBody] SetAvailableTicketsViewModel model)
        {
            string userId = this.GetUserId();
            bool isUserManager = await this.managerService.IsUserAManager(userId);
            if (!isUserManager)
            {
                return Unauthorized("Only Managers can access this endpoint.");
            }

            if(ModelState.IsValid == false)
            {
                return BadRequest("Invalid data. Please ensure all required fields are provided and valid.");
            }
           

            bool result = await this.ticketService.SetAvailableTicketsAsync(model.CinemaId, model.MovieId, model.AvailableTickets);
            if (!result)
            {
                return BadRequest("Failed to update available tickets. Please check the provided cinema and movie IDs.");
            }

            return Ok("Available tickets updated successfully.");
        }
    }
}
