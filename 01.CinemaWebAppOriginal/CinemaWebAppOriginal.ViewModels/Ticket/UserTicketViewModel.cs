using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaWebAppOriginal.ViewModels.Ticket
{
    public class UserTicketViewModel
    {
        public string TicketId { get; set; } = null!;
        public string MovieTitle { get; set; } = null!;
        public string CinemaName { get; set; } = null!;
        public string Price { get; set; } = null!;
        public string Location { get; set; } = null!;



    }
}
