using CinemaWebAppOriginal.Data.Models;
using CinemaWebAppOriginal.Infrastructure.Repositories.Contracts;
using CinemaWebAppOriginal.Services.Data.Interfaces;
using CinemaWebAppOriginal.ViewModels.Ticket;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebAppOriginal.Services.Data
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<CinemaMovie, object> cinemaMovieRepository;

        public TicketService(IRepository<CinemaMovie, object> _cinemaMovieRepository)
        {
            cinemaMovieRepository = _cinemaMovieRepository;
        }

        public Task<bool> BuyTicketAsync(int cinemaId, int movieId, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DecreaseAvailableTicketsAsync(int cinemaId, int movieId, int numberOfTickets)
        {
            CinemaMovie? cinemaMovie = await this.cinemaMovieRepository.GetAllAttached()
                .FirstOrDefaultAsync(cm => cm.CinemaId == cinemaId && cm.MovieId == movieId);

            if (cinemaMovie == null || cinemaMovie.IsDeleted == true || (cinemaMovie.AvailableTickets - numberOfTickets) < 0)
            {
                return false;
            }

            cinemaMovie.AvailableTickets -= numberOfTickets;
            await this.cinemaMovieRepository.UpdateAndSaveAsync(cinemaMovie);

            return true;
        }

        public Task<IEnumerable<UserTicketViewModel>> GetUserTicketsAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SetAvailableTicketsAsync(int cinemaId, int movieId, int availableTickets)
        {
            CinemaMovie? cinemaMovie = await this.cinemaMovieRepository.GetAllAttached()
                .FirstOrDefaultAsync(cm => cm.CinemaId == cinemaId && cm.MovieId == movieId);

            if (cinemaMovie == null || cinemaMovie.IsDeleted == true)
            {
                return false;
            }

            cinemaMovie.AvailableTickets = availableTickets;

            await this.cinemaMovieRepository.UpdateAndSaveAsync(cinemaMovie);

            return true;
        }

        public async Task<bool> SetAvailableTicketsAsync(SetAvailableTicketsViewModel viewModel)
        {
            CinemaMovie ?cinemaMovie = await this.cinemaMovieRepository.GetAllAttached()
                .FirstOrDefaultAsync(cm => cm.CinemaId == viewModel.CinemaId && cm.MovieId == viewModel.MovieId);

             if (cinemaMovie == null || cinemaMovie.IsDeleted == true)
             {
                 return false;
             }

            cinemaMovie.AvailableTickets = viewModel.AvailableTickets;
            
            await this.cinemaMovieRepository.UpdateAndSaveAsync(cinemaMovie);

            return true;
        }
    }
}
