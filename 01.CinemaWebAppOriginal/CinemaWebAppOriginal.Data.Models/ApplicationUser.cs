using Microsoft.AspNetCore.Identity;

namespace CinemaWebAppOriginal.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public virtual ICollection<UserMovie> Watchlist { get; set; } = new List<UserMovie>();
        public virtual Manager Manager { get; set; } = null!;
    }
}
