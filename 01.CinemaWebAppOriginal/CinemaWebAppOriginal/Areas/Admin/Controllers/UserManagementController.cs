

using Microsoft.AspNetCore.Mvc;

namespace CinemaWebAppOriginal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
