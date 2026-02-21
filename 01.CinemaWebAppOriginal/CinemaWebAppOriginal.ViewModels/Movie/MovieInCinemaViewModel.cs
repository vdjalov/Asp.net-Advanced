namespace CinemaWebAppOriginal.ViewModels.Movie
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
