using CinemaWebAppOriginal.ViewModels.Movie;

namespace CinemaWebAppOriginal.ViewModels.Cinema
{
    public class CinemaProgramViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;

        public ICollection<MovieInCinemaViewModel> Movies { get; set; } = new HashSet<MovieInCinemaViewModel>();

    }
}
