using Microsoft.AspNetCore.Identity;

namespace DutLecturerBooking.Data
{
    public class ApplicationUser: IdentityUser
    {
        public int UserId {  get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Faculty {  get; set; }
        public string Department {  get; set; }
        public string CourseName {  get; set; }
       
    }
}
