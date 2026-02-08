using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CinemaWebAppOriginal.ViewModels
{
    public class AllMoviesViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public DateTime ReleaseDate { get; set; } = DateTime.Today;
        public string Director { get; set; } = null!;
        public int Duration { get; set; }
    }
}
