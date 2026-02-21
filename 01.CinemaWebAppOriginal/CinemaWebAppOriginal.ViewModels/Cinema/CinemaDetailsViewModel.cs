using CinemaWebAppOriginal.ViewModels.Movie;

namespace CinemaWebAppOriginal.ViewModels.Cinema
{
    public class CinemaDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public List<MovieProgramViewModel> Movies { get; set; } = new List<MovieProgramViewModel>();
    }
}
