using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DutLecturerBooking.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Faculty { get; set; }
        public string Department { get; set; }

    }
}
