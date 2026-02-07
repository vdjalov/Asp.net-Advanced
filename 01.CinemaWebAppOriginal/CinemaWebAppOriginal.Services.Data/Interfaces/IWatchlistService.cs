using CinemaWebAppOriginal.ViewModels;

namespace CinemaWebAppOriginal.Services.Data.Interfaces
{
    public interface IWatchlistService
    {
        Task<ICollection<WatchlistViewModel>> GetAllWatchlistMoviesForUserAsync(string userId);
        Task AddMovieToUserWatchlistAsync(int movieId, string userId);
        Task<bool> CheckIfMovieAlreadyAddedInWatchlistAync(int movieId, string userId);
        Task RemoveMovieFromUserWatchlistAsync(int movieId, string userId);

    }
}
