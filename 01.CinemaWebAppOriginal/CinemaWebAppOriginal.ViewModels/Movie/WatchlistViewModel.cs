using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CinemaWebAppOriginal.ViewModels
{
    public class WatchlistViewModel
    {

        public int MovieId { get; set; }
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string ReleaseDate { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;
    }
}
