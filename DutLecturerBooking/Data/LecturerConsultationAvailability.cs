using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DutLecturerBooking.Data
{
    public class LecturerConsultationAvailability
    {
        [Key]
        public int ConsultationId { get; set; }

        public DateTime ConsultationDate { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Modules")]
        public int ModuleId { get; set; }
        public Modules Modules { get; set; }
    }

}
