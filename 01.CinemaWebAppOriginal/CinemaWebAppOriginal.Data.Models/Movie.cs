using System.ComponentModel.DataAnnotations;

namespace CinemaWebAppOriginal.Data.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }

        public string Director { get; set; } = null!;

        public int Duration { get; set; }

        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;

        //navigation property for many to many relationship with cinema
        public ICollection<CinemaMovie> CinemaMovies { get; set; } = new List<CinemaMovie>();

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
