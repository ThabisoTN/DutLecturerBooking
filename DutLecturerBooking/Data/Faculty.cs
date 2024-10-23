using System.ComponentModel.DataAnnotations;

namespace DutLecturerBooking.Data
{
    public class Faculty
    {
        [Key] 
        public int FacultyId { get; set; }

        public string FacultyName { get; set; }

    }
}
