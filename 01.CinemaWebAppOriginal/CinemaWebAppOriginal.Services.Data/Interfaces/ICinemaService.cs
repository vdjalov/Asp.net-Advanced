using CinemaWebAppOriginal.ViewModels.Cinema;

namespace CinemaWebAppOriginal.Services.Data.Interfaces
{
    public interface ICinemaService
    {
        Task<IEnumerable<AllCinemaViewModel>> GetAllOrderedByLocationAsync();
        Task CreateCinemaAsync(CinemaCreateViewModel model);
        Task<CinemaProgramViewModel> GetCinemaDetailsByIdAsync(int id);
        Task<CinemaEditViewModel> EditCinemaByIdAsync(int id);
        Task EditPostCinemaByIdAsync(CinemaEditViewModel model);
        Task<bool> CheckIfCinemaExists(int id);
        Task<bool> SoftDeleteCinemaAsync(int id);
        Task<CinemaProgramViewModel> GetCinemaProgramByIdAsync(int id);
    }
}
