using System.ComponentModel.DataAnnotations;

namespace CinemaWebAppOriginal.ViewModels
{
    public class CinemaCreateViewModel
    {
        [Required]
        [StringLength(80, ErrorMessage ="Cinema name is too long.")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(50, ErrorMessage = "Location is too long.")]
        public string Location { get; set; } = null!;

    }
}
