using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DutLecturerBooking.Data
{
    public class LecturerModules
    {
        [Key]
        public int LecturerModuleId { get; set; }

        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int CourseId {  get; set; }
        public Course Course { get; set; }

        public int ModuleId { get; set; }
        public Modules Modules { get; set; }


    }

}
