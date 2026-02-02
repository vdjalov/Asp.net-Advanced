using Microsoft.AspNetCore.Mvc;

namespace CinemaWebAppOriginal.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.Message = "Welcome to the cinema web app";

            return View();
        }


    }
}
