using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CinemaWebAppOriginal.Data.Models
{
    public class Manager
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Range(7, 15)]
        public short WorkPhoneNumber { get; set; }
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;
    }
}
