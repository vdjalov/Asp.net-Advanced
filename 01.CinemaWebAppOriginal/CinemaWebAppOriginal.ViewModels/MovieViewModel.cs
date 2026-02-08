using System.ComponentModel.DataAnnotations;

namespace CinemaWebAppOriginal.ViewModels
{
    public class MovieViewModel
    {


        [Required(ErrorMessage = "Movie title is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Movie name must be between {2} and {1} letters")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage ="Genre is required")]
        public string Genre { get; set; } = null!;

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Director name is required.")]
        [StringLength(100, MinimumLength =2, ErrorMessage = "Movie name must be between {2} and {1} letters")]
        public string Director { get; set; } = null!;

        [Range(20, 250)]
        public int Duration { get; set; }

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;


    }
}
