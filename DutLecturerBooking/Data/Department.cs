namespace DutLecturerBooking.Data
{
    public class Department
    {
        public int DepartmentId {  get; set; }
        public int FacultyId { set; get; }
        public String DepartmentName { set;get; }
        public Faculty Faculty { set; get; }
    }
}
