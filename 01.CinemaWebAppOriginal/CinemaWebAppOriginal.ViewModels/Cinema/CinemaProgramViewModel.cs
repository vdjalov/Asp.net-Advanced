using CinemaWebAppOriginal.ViewModels.Movie;
using System;
using System.Collections.Generic;
using System.Text;

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
