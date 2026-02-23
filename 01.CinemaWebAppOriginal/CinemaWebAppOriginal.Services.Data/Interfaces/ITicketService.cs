using CinemaWebAppOriginal.ViewModels.Ticket;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaWebAppOriginal.Services.Data.Interfaces
{
    public interface ITicketService
    {
        Task<bool> BuyTicketAsync(int cinemaId, int movieId, string userId);
        Task<bool> DecreaseAvailableTicketsAsync(int cinemaId, int movieId, int numberOfTickets);
        Task<IEnumerable<UserTicketViewModel>> GetUserTicketsAsync(Guid userId);
        Task<bool> SetAvailableTicketsAsync(int cinemaId, int movieId, int availableTickets);
        Task<bool> SetAvailableTicketsAsync(SetAvailableTicketsViewModel viewModel);



    }
}
