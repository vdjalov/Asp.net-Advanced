using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CinemaWebAppOriginal.Data.Models
{
    public class BuyTicketRequest
    {
        [Required]
        public int CinemaId { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select at least one ticket.")]
        public int Quantity { get; set; }
    }
}
