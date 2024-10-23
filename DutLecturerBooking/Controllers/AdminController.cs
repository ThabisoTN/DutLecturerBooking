using Microsoft.AspNetCore.Mvc;

namespace DutLecturerBooking.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
