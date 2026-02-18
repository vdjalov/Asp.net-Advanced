using CinemaWebAppOriginal.Services.Data.Interfaces;
using CinemaWebAppOriginal.ViewModels.Ticket;

namespace CinemaWebAppOriginal.Services.Data
{
    public class TicketService : ITicketService
    {



        public Task<bool> BuyTicketAsync(string cinemaId, string movieId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DecreaseAvailableTicketsAsync(string cinemaId, string movieId, int numberOfTickets)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserTicketViewModel>> GetUserTicketsAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAvailableTicketsAsync(string cinemaId, string movieId, int availableTickets)
        {
            throw new NotImplementedException();
        }
    }
}
