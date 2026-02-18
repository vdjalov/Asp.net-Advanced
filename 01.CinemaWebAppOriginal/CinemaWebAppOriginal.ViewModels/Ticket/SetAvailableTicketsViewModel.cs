using System.ComponentModel.DataAnnotations;
using static CinemaWebAppOriginal.Common.ApplicationConstants.TicketConstants.TicketConstants;
using static CinemaWebAppOriginal.Common.ApplicationConstants.TicketConstants.TicketValidationMassages;

namespace CinemaWebAppOriginal.ViewModels.Ticket
{
    public class SetAvailableTicketsViewModel
    {
        [Required]
        public string CinemaId { get; set; } = null!;
        [Required]
        public string MovieId { get; set; } = null!;

        [Required(ErrorMessage = AvailableTicketsRequiredMessage)]
        [Range(MinAvailableTickets, MaxAvailableTickets, ErrorMessage = InvalidAvailableTicketsMessage)]
        public int AvailableTickets { get; set; }

    }
}
