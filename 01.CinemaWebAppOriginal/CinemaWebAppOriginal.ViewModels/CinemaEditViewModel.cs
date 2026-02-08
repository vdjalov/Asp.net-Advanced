using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CinemaWebAppOriginal.ViewModels
{
    public class CinemaEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "Cinema name is too long.")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(50, ErrorMessage = "Location is too long.")]
        public string Location { get; set; } = null!;
    }
}
