using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DutLecturerBooking.Data
{
    public class Course
    {
        [Key]
        public int CourseId {set;get; }

        public string CourseName { get; set; }

        public int Departmentid { get; set; }
        public Department Department { get; set; }
    }
}
