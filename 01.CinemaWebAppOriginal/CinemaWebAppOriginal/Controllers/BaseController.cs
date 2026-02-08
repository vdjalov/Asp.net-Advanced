using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CinemaWebAppOriginal.Controllers
{
    public class BaseController : Controller
    {
        // Method to get the user id from the claims
        protected string GetUserId()
           => User.FindFirstValue(ClaimTypes.NameIdentifier);



    }
}
