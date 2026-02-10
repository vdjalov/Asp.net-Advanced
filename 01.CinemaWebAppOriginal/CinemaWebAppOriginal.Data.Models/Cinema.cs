using System.ComponentModel.DataAnnotations;

namespace CinemaWebAppOriginal.Data.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Location { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;

        //navigation property for many to many relationship with movie
        public ICollection<CinemaMovie> CinemaMovies { get; set; } = new List<CinemaMovie>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
       
    }
}
