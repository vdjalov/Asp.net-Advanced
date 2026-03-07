using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaWebAppOriginal.Services.Data
{
    public class EmailSender : IEmailSender // dummy class email does not work no idea why 
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
           return Task.CompletedTask; // done on purpose not to bother with email sending for now, but the class is here if we want to implement it in the future
        }
    }
}
