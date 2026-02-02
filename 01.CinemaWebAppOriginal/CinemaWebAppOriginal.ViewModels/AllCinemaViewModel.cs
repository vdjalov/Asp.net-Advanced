using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CinemaWebAppOriginal.ViewModels
{
    public class AllCinemaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Cinema name is required.")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "Cinema name is too long.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Location is too long.")]
        public string Location { get; set; } = null!;
    }
}
