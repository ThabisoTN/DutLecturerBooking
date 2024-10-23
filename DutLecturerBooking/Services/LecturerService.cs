using DutLecturerBooking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // For logging

namespace DutLecturerBooking.Services
{
    public class LecturerService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LecturerService> _logger;

        
        public LecturerService(ApplicationDbContext context, ILogger<LecturerService> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Save a new lecturer module
        public async Task<bool> SaveLecturerModuleAsync(string userId, int courseId, int moduleId)
        {
            try
            {
                // Check if the lecturer is already assigned to this module for the course
                var existingLecturerModule = await _context.LecturerModules.FirstOrDefaultAsync(lm => lm.UserId == userId /*&& lm.CourseId == courseId*/ && lm.ModuleId == moduleId);

                if (existingLecturerModule != null)
                {
                    return false;
                }

                // Create a new LecturerModules object
                var lecturerModule = new LecturerModules
                {
                    UserId = userId,
                    //CourseId = courseId,
                    ModuleId = moduleId,
                };

              
                _context.LecturerModules.Add(lecturerModule);
                await _context.SaveChangesAsync();

                return true; 
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "An error occurred while saving the lecturer module.");
                return false;
            }
        }

        // Retrieve all modules taught by a specific lecturer
        public async Task<List<LecturerModules>> GetModulesForLecturerAsync(string userId)
        {
            try
            {
                return await _context.LecturerModules /*.Include(lm => lm.Course)*/.Include(lm => lm.Modules).Where(lm => lm.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {
              
                _logger.LogError(ex, "An error occurred while retrieving the lecturer's modules.");
                return new List<LecturerModules>(); 
            }
        }

   
    }
}
