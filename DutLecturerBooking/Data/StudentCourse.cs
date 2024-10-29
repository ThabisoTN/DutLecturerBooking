using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace DutLecturerBooking.Data
{
    public class StudentCourse
    {

        [Key]
        public int StudentCourseId { get; set; }

        public string UserId { get; set; } 
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }

    }
}
