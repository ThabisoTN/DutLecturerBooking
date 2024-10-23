using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DutLecturerBooking.Data
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]  
        public string DepartmentName { get; set; }


        public int FacultyId { get; set; }

        public Faculty Faculty { get; set; }

    }
}
