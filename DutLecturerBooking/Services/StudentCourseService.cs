//using DutLecturerBooking.Data;
//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;

//namespace DutLecturerBooking.Services
//{
//    public class StudentCourseService
//    {
//        private readonly ApplicationDbContext _context;

//        public StudentCourseService(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<string> RegisterCourseAsync(int userId, int courseId)
//        {
//            // Check if the student is already registered for a course
//            var existingRegistration = await _context.StudentCourses.FirstOrDefaultAsync(sc => sc.UserId == userId);

//            if (existingRegistration != null)
//            {
//                return "Student is already registered for a course.";
//            }

//            // If not registered, create a new StudentCourse record
//            var newRegistration = new StudentCourse
//            {
//                UserId = userId,
//                CourseId = courseId
//            };

//            _context.StudentCourses.Add(newRegistration);
//            await _context.SaveChangesAsync();

//            return "Course registered successfully!";
//        }
//    }
//}
