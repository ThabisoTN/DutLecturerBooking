using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DutLecturerBooking.Data
{
    public class StudentModules
    {
        [Key]
        public int StudentModuleId { get; set; }

        // Foreign key to Modules table
        public int ModuleId { get; set; }

        [ForeignKey("ModuleId")]
        public Modules Modules { get; set; }

        // Foreign key to ApplicationUser (Student)
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
