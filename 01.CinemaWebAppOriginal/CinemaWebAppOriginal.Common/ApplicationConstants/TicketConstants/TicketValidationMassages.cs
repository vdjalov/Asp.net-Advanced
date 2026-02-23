using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaWebAppOriginal.Common.ApplicationConstants.TicketConstants
{
    public static class TicketValidationMassages
    {
        public const string InvalidNumberOfTicketsMessage = "The number of tickets must be between 1 and 10.";
        public const string InvalidPriceMessage = $"The price must be a positive amount between {TicketConstants.MinPrice} and {TicketConstants.MaxPrice}.";
        public const string AvailableTicketsRequiredMessage = "Please enter the number of available tickets.";
        public const string InvalidAvailableTicketsMessage = "Available tickets must be a positive number between.";

    }
}
