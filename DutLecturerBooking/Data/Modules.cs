using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DutLecturerBooking.Data
{
    public class Modules
    {
        [Key] 
        public int ModuleId { get; set; }

        [Required] 
        [MaxLength(100)] 
        public string ModuleName { get; set; }

        public int CourseId {  get; set; }
        public Course Course { get; set; }


    }
}
