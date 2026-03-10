using CinemaWebAppOriginal.Services.Data.Interfaces;
using CinemaWebAppOriginal.ViewModels.Cinema;
using CinemaWebAppOriginal.ViewModels.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CInemaWebAppOriginal.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   //[Authorize]
    public class TicketApiController : ControllerBase
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
            //string userId = this.GetUserId();
            //bool isUserManager = await this.managerService.IsUserAManager(userId);
            //if (!isUserManager)
            //{
            //    return Unauthorized("Only Managers can access this endpoint.");
            //}

            CinemaProgramViewModel movies = await this.cinemaService.GetCinemaProgramByIdAsync(cinemaId);

            return Ok(movies);
        }

        [HttpPost("UpdateAvailableTickets")]
        public async Task<IActionResult> UpdateAvailableTickets([FromBody] SetAvailableTicketsViewModel model)
        {
            //string userId = this.GetUserId();
            //bool isUserManager = await this.managerService.IsUserAManager(userId);
            //if (!isUserManager)
            //{
            //    return Unauthorized("Only Managers can access this endpoint.");
            //}

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

        [HttpPost("BuyTicket")]
        public async Task<IActionResult> BuyTicket([FromBody] BuyTicketViewModel model)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest("Invalid data. Please ensure all required fields are provided and valid.");
            }

            string userId = this.GetUserId();

            if(string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized("User must be authenticated to buy a ticket.");
            }

            Guid guidId = Guid.Parse(userId);

            bool isUserManager = await this.managerService.IsUserAManager(guidId);

            if (!isUserManager)
            {
                return Unauthorized("Only Managers can access this endpoint.");
            }

            bool result = await this.ticketService.BuyTicketAsync(model,guidId);

            if (!result)
            {
                return BadRequest("Failed to buy ticket. Please check the provided cinema and movie IDs, and ensure there are available tickets.");
            }
            return Ok("Ticket bought successfully.");
        }




        // Method to get the user id from the claims
        private string GetUserId()
           => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
