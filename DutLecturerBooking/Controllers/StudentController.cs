using Microsoft.AspNetCore.Mvc;

namespace DutLecturerBooking.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
