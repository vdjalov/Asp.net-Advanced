using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaWebAppOriginal.Data.Configurations
{
    public class MovieConfigure
    {
        public void Configure(EntityEntry)
        {
            // This class is intentionally left empty as the configuration for the Movie entity is handled through data annotations in the Movie class itself.
        }
    }
}
