using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
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

            //seed mudules
            if (!context.Modules.Any())
            {
                await SeedModulesAsync(context);
            }

            ////seed courses
            if (!context.Courses.Any())
            {
                await SeedCoursesAsync(context);
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
                new Faculty {  FacultyName = "Faculty of Accounting and Informatics" },
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
                new Department { FacultyId = 1, DepartmentName = "Mechanical Engineering" },
            
                
                // Departments under Faculty of Accounting and Informatics (FacultyId = 2)
                new Department { FacultyId = 2, DepartmentName = "Information Technology" },
               
                
                // Departments under Faculty of Health Sciences (FacultyId = 3)
               
                new Department { FacultyId = 3, DepartmentName = "Radiography" },
             
                
                // Departments under Faculty of Tourism and Hospitality (FacultyId = 4)
                new Department { FacultyId = 4, DepartmentName = "Hospitality and Tourism" },
               
                
                // Departments under Faculty of Applied Science (FacultyId = 5)
                new Department { FacultyId = 5, DepartmentName = "Biotechnology" },
              
                
                // Departments under Faculty of Arts and Design (FacultyId = 6)
                new Department { FacultyId = 6, DepartmentName = "Drama and Production Studies" },
              
                
                // Departments under Faculty of Management Sciences (FacultyId = 7)
             
                new Department { FacultyId = 7, DepartmentName = "Hospitality and Tourism Management" },
              
            };

            await context.Departments.AddRangeAsync(departments);
            await context.SaveChangesAsync();
            Console.WriteLine("Departments seeded successfully.");
        }

        //seeding courses
        private static async Task SeedCoursesAsync(ApplicationDbContext context)
        {
            if (!context.Courses.Any())
            {
                var courses = new[]
                {
                    new Course { CourseName = "Advance Diploma in ICT", Departmentid = 2 }
                };
                
                await context.Courses.AddRangeAsync(courses);
                await context.SaveChangesAsync();
                Console.WriteLine("Courses seeded successfully.");
            }
            else
            {
                Console.WriteLine("Courses already exist.");
            }
        }



        //Seed modules. 
        private static async Task SeedModulesAsync(ApplicationDbContext context)
        {
            // Ensure at least one course exists before seeding modules
            var course = await context.Courses.FirstOrDefaultAsync(c => c.CourseName == "Advance Diploma in ICT");

            if (course != null)
            {
                var modules = new[]
                {
                    new Modules { ModuleName = "Data Structures", CourseId = 1 },
                    new Modules { ModuleName = "Software Development and Management", CourseId = 1},
                    new Modules { ModuleName = "Applied Mathematics for Computing", CourseId = 1 },
                    new Modules { ModuleName = "Machine Intelligence", CourseId = 1 },
                    new Modules { ModuleName = "Business Intelligence", CourseId =  1 }
                };

                await context.Modules.AddRangeAsync(modules);
                await context.SaveChangesAsync();
                Console.WriteLine("Modules seeded successfully.");
            }
            else
            {
                Console.WriteLine("Error: No course found. Modules cannot be seeded without an existing course.");
            }
        }
    }
}
