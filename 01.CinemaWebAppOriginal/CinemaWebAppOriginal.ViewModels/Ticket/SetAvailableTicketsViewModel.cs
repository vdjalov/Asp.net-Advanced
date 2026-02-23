using System.ComponentModel.DataAnnotations;
using static CinemaWebAppOriginal.Common.ApplicationConstants.TicketConstants.TicketConstants;
using static CinemaWebAppOriginal.Common.ApplicationConstants.TicketConstants.TicketValidationMassages;

namespace CinemaWebAppOriginal.ViewModels.Ticket
{
    public class SetAvailableTicketsViewModel
    {
        [Required]
        public int CinemaId { get; set; }
        [Required]
        public int MovieId { get; set; }

        [Required(ErrorMessage = AvailableTicketsRequiredMessage)]
        [Range(MinAvailableTickets, MaxAvailableTickets, ErrorMessage = InvalidAvailableTicketsMessage)]
        public int AvailableTickets { get; set; }

    }
}
