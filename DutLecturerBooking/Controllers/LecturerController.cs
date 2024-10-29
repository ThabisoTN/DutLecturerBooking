using DutLecturerBooking.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using DutLecturerBooking.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DutLecturerBooking.Controllers
{
    public class LecturerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly LecturerService _lecturerService;

        public LecturerController(ApplicationDbContext context, LecturerService lecturerService)
        {
            _context = context;
            _lecturerService = lecturerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Manage modules for the lecturer
        public async Task<IActionResult> ManageLecturerModules()
        {
            var lecturerModules = await _context.LecturerModules.Include(lm => lm.Modules) .Include(lm => lm.Course) .ToListAsync();

            return View(lecturerModules);
        }


        [HttpGet]
        public async Task<IActionResult> SaveLecturerModule()
        {
            var courses = await _context.Courses.ToListAsync(); 
            ViewBag.Courses = courses;

            return View();
        }

        // This method will fetch modules dynamically based on the selected course
        [HttpGet]
        public async Task<IActionResult> GetModulesByCourse(int courseId)
        {
            var modules = await _context.Modules.Where(m => m.CourseId == courseId).ToListAsync(); 

            return Json(modules);
        }




        // POST: Save a new module the lecturer teaches
        [HttpPost]
        public async Task<IActionResult> SaveLecturerModule(int courseId, int moduleId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not logged in.");
            }
            var courseExists = await _context.Courses.AnyAsync(c => c.CourseId == courseId);
            var moduleExists = await _context.Modules.AnyAsync(m => m.ModuleId == moduleId && m.CourseId == courseId);

            if (!courseExists || !moduleExists)
            {
                TempData["ErrorMessage"] = "Selected course or module does not exist.";
                return RedirectToAction(nameof(SaveLecturerModule));
            }

            var lecturerModule = new LecturerModules
            {
                UserId = userId,
                CourseId = courseId,
                ModuleId = moduleId
            };

            _context.LecturerModules.Add(lecturerModule);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Module assigned successfully.";
            return RedirectToAction(nameof(ManageLecturerModules));
        }


        public async Task<IActionResult> ManageAvailability()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            
            var availabilities = await _context.lecturerConsultationAvailabilities.Include(l => l.Modules) .Where(l => l.UserId == userId).ToListAsync();

            return View(availabilities); 
        }


        //set availability
        public async Task<IActionResult> SetAvailability()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the logged-in lecturer's ID

            // Fetch the modules that the lecturer teaches
            var lecturerModules = await _context.LecturerModules.Include(lm => lm.Modules).Where(lm => lm.UserId == userId).ToListAsync();

            ViewBag.Modules = new SelectList(lecturerModules.Select(lm => lm.Modules), "ModuleId", "ModuleName");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SetAvailability(LecturerConsultationAvailability model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not logged in.");
            }

            // Check if the module exists in the Modules table
            var moduleExists = await _context.Modules.AnyAsync(m => m.ModuleId == model.ModuleId);
            if (!moduleExists)
            {
                ModelState.AddModelError("", "The selected module does not exist.");
                return View(model);
            }

            // Ensure the lecturer is teaching the selected module
            var lecturerModule = await _context.LecturerModules.FirstOrDefaultAsync(lm => lm.UserId == userId && lm.ModuleId == model.ModuleId);

            if (lecturerModule == null)
            {
                ModelState.AddModelError("", "You are not authorized to set availability for this module.");
                return View(model); 
            }

            // Ensure end time is after start time
            if (model.EndTime <= model.StartTime)
            {
                ModelState.AddModelError("", "End time must be after the start time.");
                return View(model);
            }

           
            var consultationAvailability = new LecturerConsultationAvailability
            {
                ConsultationDate = model.ConsultationDate,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                ModuleId = model.ModuleId,
                UserId = userId
            };

            _context.lecturerConsultationAvailabilities.Add(consultationAvailability);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Availability set successfully!";
            return RedirectToAction("ManageAvailability");
        }



    }
}
