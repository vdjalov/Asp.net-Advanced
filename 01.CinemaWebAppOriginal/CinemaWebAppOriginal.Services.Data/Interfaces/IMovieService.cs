using CinemaWebAppOriginal.ViewModels.Movie;

namespace CinemaWebAppOriginal.Services.Data.Interfaces
{
    public interface IMovieService
    {
        //Task<ICollection<AllMoviesViewModel>> GetAllMoviesAsync(string? searchQuery = null, string? genre = null, int? releaseYear = null); // index method 
        Task<(ICollection<AllMoviesViewModel> Movies, int TotalPages)> GetAllMoviesAsync(string? searchQuery = null, string? genre = null,
                                                                                int? releaseYear = null, int pageNumber = 1, int pageSize = 5);
        Task CreateMovieAsync(MovieViewModel viewModel); // create method    
        Task<MovieViewModel> GetMovieDetailsById(int id); // details method for movie from DB
        Task<AddMovieToCinemaProgramViewModel> AddMovieToCinemaProgramGetView(int movieId); // for the view 
        Task AddMovieToACinemaProgramAsync(AddMovieToCinemaProgramViewModel model);
        Task<bool> CheckIfMovieExists(int movieId); // Checking if movie exists in the db 
        Task<EditMovieViewModel> GetMovieEditModelByIdAsync(int id); // for the edit view get method
        Task<bool> UpdateMovieAsync(EditMovieViewModel viewModel); // for the edit view post method
        Task<bool> SoftDeleteMovieAsync(int id); // for the delete method
        Task<DeleteMovieViewModel> GetDeleteMovieViewModelByIdAsync(int id); // for the delete view get method
        Task<ICollection<AllMoviesViewModel>> GetOnlyAllMoviesAsync();
       


    }
}
