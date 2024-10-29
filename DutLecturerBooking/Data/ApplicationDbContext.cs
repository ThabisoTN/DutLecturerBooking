using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DutLecturerBooking.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Modules> Modules { get; set; }
        public DbSet<LecturerModules> LecturerModules { get; set; }
        public DbSet<LecturerConsultationAvailability> lecturerConsultationAvailabilities { set; get; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<StudentModules> StudentModules { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // LecturerModules and ApplicationUser relationship
            modelBuilder.Entity<LecturerModules>()
                .HasOne(lm => lm.ApplicationUser)
                .WithMany()
                .HasForeignKey(lm => lm.UserId)
                .OnDelete(DeleteBehavior.Restrict); // No cascading delete for User

            // LecturerModules and Modules relationship (change this to Restrict or NoAction)
            modelBuilder.Entity<LecturerModules>()
                .HasOne(lm => lm.Modules)
                .WithMany()
                .HasForeignKey(lm => lm.ModuleId)
                .OnDelete(DeleteBehavior.Restrict); // Change to Restrict to prevent multiple cascade paths

            // LecturerModules and Course relationship (change this to Restrict or NoAction)
            modelBuilder.Entity<LecturerModules>()
                .HasOne(lm => lm.Course)
                .WithMany()
                .HasForeignKey(lm => lm.CourseId)
                .OnDelete(DeleteBehavior.Restrict); // Change to Restrict to prevent multiple cascade paths
        }




    }


}

