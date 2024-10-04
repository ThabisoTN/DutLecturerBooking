using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DutLecturerBooking.Data
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Apply any pending migrations
            await context.Database.MigrateAsync();

            // Seed roles
            if (!roleManager.Roles.Any())
            {
                await SeedRolesAsync(roleManager);
            }

            // Seed faculties if not already present
            if (!context.Faculties.Any())
            {
                await SeedFacultiesAsync(context); 
            }

            //seed departments
            if (!context.Departments.Any())
            {
                await SeedDepartmentsAsync(context);
            }
        }

        // Seeding roles method
        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Admin", "Student","Lecturer" };

            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new IdentityRole(roleName);
                    var result = await roleManager.CreateAsync(role);
                    if (!result.Succeeded)
                    {
                        Console.WriteLine($"Error creating role {roleName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }
        }

        // Seed faculties method 
        private static async Task SeedFacultiesAsync(ApplicationDbContext context)
        {
            var faculties = new[]
            {
                new Faculty {  FacultyName = "Faculty of Engineering and the Built Environmentg" },
                new Faculty { FacultyName = "Faculty of Accounting and Informatics" },
                new Faculty {  FacultyName = "Faculty of Health Sciences" },
                new Faculty {  FacultyName = "Faculty of Tourism and Hospitality" },
                new Faculty {  FacultyName = "Faculty of Applied Science" },
                new Faculty {  FacultyName = "Faculty of Arts and Design" },
                new Faculty {  FacultyName = "Faculty of Management Sciences" }

            };

            await context.Faculties.AddRangeAsync(faculties);
            await context.SaveChangesAsync();
            Console.WriteLine("Faculties seeded successfully.");
        }

        private static async Task SeedDepartmentsAsync(ApplicationDbContext context)
        {
            var departments = new[]
            {
                // Departments under Faculty of Engineering and the Built Environment (FacultyId = 1)
                new Department { FacultyId = 1, DepartmentName = "Civil Engineering" },
                new Department { FacultyId = 1, DepartmentName = "Electrical Power Engineering" },
                new Department { FacultyId = 1, DepartmentName = "Mechanical Engineering" },
                new Department { FacultyId = 1, DepartmentName = "Town and Regional Planning" },
                new Department { FacultyId = 1, DepartmentName = "Architecture" },
                new Department { FacultyId = 1, DepartmentName = "Chemical Engineering" },
                new Department { FacultyId = 1, DepartmentName = "Electronic Engineering" },
                new Department { FacultyId = 1, DepartmentName = "Industrial Engineering" },
                
                
                // Departments under Faculty of Accounting and Informatics (FacultyId = 2)
                new Department { FacultyId = 2, DepartmentName = "Information Technology" },
                new Department { FacultyId = 2, DepartmentName = "Accounting" },
                new Department { FacultyId = 2, DepartmentName = "Auditing" },
                new Department { FacultyId = 2, DepartmentName = "Financial Accounting" },
                new Department { FacultyId = 2, DepartmentName = "Management Accounting" },
                new Department { FacultyId = 2, DepartmentName = "Informatics" },
                
                // Departments under Faculty of Health Sciences (FacultyId = 3)
                new Department { FacultyId = 3, DepartmentName = "Nursing" },
                new Department { FacultyId = 3, DepartmentName = "Radiography" },
                new Department { FacultyId = 3, DepartmentName = "Chiropractic" },
                new Department { FacultyId = 3, DepartmentName = "Dental Sciences" },
                new Department { FacultyId = 3, DepartmentName = "Emergency Medical Care and Rescue" },
                new Department { FacultyId = 3, DepartmentName = "Community Health Studies" },
                
                // Departments under Faculty of Tourism and Hospitality (FacultyId = 4)
                new Department { FacultyId = 4, DepartmentName = "Hospitality and Tourism" },
                new Department { FacultyId = 4, DepartmentName = "Culinary Arts" },
                
                // Departments under Faculty of Applied Science (FacultyId = 5)
                new Department { FacultyId = 5, DepartmentName = "Biotechnology" },
                new Department { FacultyId = 5, DepartmentName = "Chemistry" },
                new Department { FacultyId = 5, DepartmentName = "Food Science and Technology" },
                new Department { FacultyId = 5, DepartmentName = "Mathematics" },
                new Department { FacultyId = 5, DepartmentName = "Horticulture" },
                new Department { FacultyId = 5, DepartmentName = "Physics" },
                
                // Departments under Faculty of Arts and Design (FacultyId = 6)
                new Department { FacultyId = 6, DepartmentName = "Drama and Production Studies" },
                new Department { FacultyId = 6, DepartmentName = "Fashion and Textiles" },
                new Department { FacultyId = 6, DepartmentName = "Fine Art and Jewellery Design" },
                new Department { FacultyId = 6, DepartmentName = "Graphic Design" },
                new Department { FacultyId = 6, DepartmentName = "Interior Design" },
                new Department { FacultyId = 6, DepartmentName = "Journalism" },
                
                // Departments under Faculty of Management Sciences (FacultyId = 7)
                new Department { FacultyId = 7, DepartmentName = "Business Studies Unit" },
                new Department { FacultyId = 7, DepartmentName = "Entrepreneurial Studies and Management" },
                new Department { FacultyId = 7, DepartmentName = "Hospitality and Tourism Management" },
                new Department {  FacultyId = 7, DepartmentName = "Human Resources Management" },
                new Department {  FacultyId = 7, DepartmentName = "Marketing and Retail Management" },
                new Department {  FacultyId = 7, DepartmentName = "Operations and Quality Management" },
                new Department {  FacultyId = 7, DepartmentName = "Public Management and Economics" },
                new Department {  FacultyId = 7, DepartmentName = "Sport Studies" }
            };

            await context.Departments.AddRangeAsync(departments);
            await context.SaveChangesAsync();
            Console.WriteLine("Departments seeded successfully.");
        }
    }
}
