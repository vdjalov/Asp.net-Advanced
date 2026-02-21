using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaWebAppOriginal.ViewModels
{
    public class MovieInCinemaViewModel
    {
        public int Id { get; set; }
        public int CinemaId { get; set; }
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string Duration { get; set; } = null!; 
        public string Description { get; set; } = null!;
        public int AvailableTickets { get; set; }
    }
}
