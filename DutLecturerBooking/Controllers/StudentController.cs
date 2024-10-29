using DutLecturerBooking.Data;
using DutLecturerBooking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DutLecturerBooking.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ManageCourse()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized("User not logged in.");
            }

            // Check if the student is already registered for a course
            var studentCourse = await _context.StudentCourses
                .Include(sc => sc.Course)
                .ThenInclude(c => c.Department) // Include Department if used in the view
                .FirstOrDefaultAsync(sc => sc.UserId == user.Id); // Assuming UserId is of type string

            if (studentCourse != null && studentCourse.Course != null)
            {
                // Pass the registered course information to the view only if it exists
                ViewBag.IsRegistered = true;
                ViewBag.RegisteredCourse = studentCourse.Course;
            }
            else
            {
                // If not registered, pass the list of available courses to the view
                ViewBag.IsRegistered = false;
                ViewBag.AvailableCourses = await _context.Courses.ToListAsync();
            }

            return View();
        }



        //Post method to register a Course 

        [HttpPost]
        public async Task<IActionResult> RegisterCourse(int courseId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized("User not logged in.");
            }

            // Check if the student is already registered for a course
            var existingCourse = await _context.StudentCourses.FirstOrDefaultAsync(sc => sc.UserId == user.Id);

            if (existingCourse != null)
            {
                TempData["ErrorMessage"] = "You are already registered for a course.";
                return RedirectToAction(nameof(ManageCourse));
            }

            // Register the student for the selected course
            var studentCourse = new StudentCourse
            {
                UserId = user.Id, // Assuming UserId is of type string
                CourseId = courseId
            };

            _context.StudentCourses.Add(studentCourse);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Course registered successfully!";
            return RedirectToAction(nameof(ManageCourse));
        }

        [HttpGet]
        public async Task<IActionResult> ManageStudentModules()
        {
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not logged in.");
            }

            
            var registeredModules = await _context.StudentModules.Where(sm => sm.UserId == userId).Include(sm => sm.Modules).ThenInclude(m => m.Course).ToListAsync();

            if (registeredModules == null || !registeredModules.Any())
            {
                TempData["InfoMessage"] = "You have not registered for any modules yet.";
                return View(new List<StudentModules>()); // Pass an empty list to the view
            }

            return View(registeredModules);
        }



        //Get method 
        [HttpGet]
        public async Task<IActionResult> RegisterStudentModules()
        {
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not logged in.");
            }

            // Retrieve the list of modules the student is not already registered for
            var registeredModuleIds = await _context.StudentModules
                .Where(sm => sm.UserId == userId)
                .Select(sm => sm.ModuleId)
                .ToListAsync();

            var availableModules = await _context.Modules
                .Where(m => !registeredModuleIds.Contains(m.ModuleId))
                .ToListAsync();

            if (availableModules == null || !availableModules.Any())
            {
                TempData["InfoMessage"] = "No available modules to register.";
                return RedirectToAction("ManageStudentModules");
            }

            return View(availableModules);
        }




        // POST: Register selected modules for the student
        [HttpPost]
        public async Task<IActionResult> RegisterStudentModules(int[] selectedModules)
        {
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not logged in.");
            }

            if (selectedModules == null || !selectedModules.Any())
            {
                TempData["ErrorMessage"] = "Please select at least one module to register.";
                return RedirectToAction("ManageStudentModules");
            }

            // Register each selected module for the student
            foreach (var moduleId in selectedModules)
            {
                var studentModule = new StudentModules
                {
                    UserId = userId,
                    ModuleId = moduleId
                };
                _context.StudentModules.Add(studentModule);
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Modules registered successfully!";
            return RedirectToAction("ManageStudentModules");
        }
    }
}
