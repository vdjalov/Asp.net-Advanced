using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaWebAppOriginal.Services.Data.Interfaces
{
    public interface IManagerService
    {
        Task<bool> IsUserAManager(string userid);
    }
}
