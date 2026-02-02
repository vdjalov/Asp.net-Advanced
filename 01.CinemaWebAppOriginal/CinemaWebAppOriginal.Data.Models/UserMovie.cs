using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaWebAppOriginal.Data.Models
{

    [PrimaryKey(nameof(UserId), nameof(MovieId))]
    public class UserMovie
    {
        [Required]
        public string UserId { get; set; } = null!; // links user who added the movie

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        public int MovieId { get; set; }

        [ForeignKey(nameof(MovieId))]
        public Movie Movie { get; set; } = null!;
    }
}
