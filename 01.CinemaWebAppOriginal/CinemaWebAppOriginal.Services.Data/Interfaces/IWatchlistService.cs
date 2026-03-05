using CinemaWebAppOriginal.ViewModels;

namespace CinemaWebAppOriginal.Services.Data.Interfaces
{
    public interface IWatchlistService
    {
        Task<ICollection<WatchlistViewModel>> GetAllWatchlistMoviesForUserAsync(Guid userId);
        Task AddMovieToUserWatchlistAsync(int movieId, Guid userId);
        Task<bool> CheckIfMovieAlreadyAddedInWatchlistAync(int movieId, Guid userId);
        Task RemoveMovieFromUserWatchlistAsync(int movieId, Guid userId);

    }
}
