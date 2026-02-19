using CinemaWebAppOriginal.Services.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebAppOriginal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketsApiController : ControllerBase
    {
        private readonly ITicketService ticketService;
        private readonly ICinemaService cinemaService;
        private readonly IManagerService managerService;

        public TicketsApiController(ITicketService _ticketService, ICinemaService _cinemaService, IManagerService _managerService)
        {
            this.ticketService = _ticketService;
            this.cinemaService = _cinemaService;
            this.managerService = _managerService;
        }






    }
}
