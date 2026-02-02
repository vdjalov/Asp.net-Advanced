using CinemaWebAppOriginal.ViewModels;

namespace CinemaWebAppOriginal.Services.Data.Interfaces
{
    public interface ICinemaService
    {
        Task<IEnumerable<AllCinemaViewModel>> GetAllOrderedByLocationAsync();
        Task CreateCinemaAsync(CinemaCreateViewModel model);
        Task<CinemaDetailsViewModel> GetDetailsByIdAsync(int id);

    }
}
