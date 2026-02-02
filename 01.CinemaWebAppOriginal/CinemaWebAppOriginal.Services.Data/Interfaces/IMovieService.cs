using CinemaWebAppOriginal.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaWebAppOriginal.Services.Data.Interfaces
{
    public interface IMovieService
    {
        Task<ICollection<AllMoviesViewModel>> GetAllMoviesAsync(); // index method 
        Task<IQueryable<MovieViewModel>> GetAllMoviesAttached(); // method for attaching movies to other entities
        Task CreateMovieAsync(MovieViewModel viewModel); // create method    

        Task<MovieViewModel> GetMovieDetailsById(int id); // details method for movie from DB
        Task AddMovieToACinemaProgram(int movieId); // for the view 
        Task AddMovieToACinemaProgram(AddMovieToCinemaProgramViewModel model);
        Task<bool> CheckIfMovieExists(int movieId); // Checking if movie exists in the db 

    }
}
