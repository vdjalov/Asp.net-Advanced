using CinemaWebAppOriginal.ViewModels.Ticket;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaWebAppOriginal.Services.Data.Interfaces
{
    public interface ITicketService
    {
        Task<bool> BuyTicketAsync(string cinemaId, string movieId, string userId);
        Task<bool> SetAvailableTicketsAsync(string cinemaId, string movieId, int availableTickets);
        Task<bool> DecreaseAvailableTicketsAsync(string cinemaId, string movieId, int numberOfTickets);
        Task<IEnumerable<UserTicketViewModel>> GetUserTicketsAsync(Guid userId);
       


    }
}
