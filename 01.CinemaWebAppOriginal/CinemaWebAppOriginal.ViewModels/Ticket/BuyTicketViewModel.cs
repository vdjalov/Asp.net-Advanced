using System.ComponentModel.DataAnnotations;
using static CinemaWebAppOriginal.Common.ApplicationConstants.TicketConstants.TicketConstants;
using static CinemaWebAppOriginal.Common.ApplicationConstants.TicketConstants.TicketValidationMassages;

namespace CinemaWebAppOriginal.ViewModels.Ticket
{
    public class BuyTicketViewModel 
    {
        public int CinemaId { get; set; }
        public int MovieId { get; set; }
        public string UserId { get; set; } = null!;

        [Required]
        [Range(MinNumberOfTickets, MaxNumberOfTickets, ErrorMessage = InvalidNumberOfTicketsMessage )]
        public int NumberOfTickets { get; set; }

        [Required]
        [Range(typeof(decimal), MinPrice, MaxPrice, ErrorMessage = InvalidPriceMessage)]
        public decimal Price { get; set; }



    }
}
