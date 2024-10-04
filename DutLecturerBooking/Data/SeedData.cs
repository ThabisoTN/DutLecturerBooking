using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
namespace DutLecturerBooking.Data
{
    public class SeedData
    {
        public  static async Task EnsureRoles(RoleManager<IdentityRole>roleManager, string roles)
        {
            foreach(var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(roles))
                {
                    await roleManager.CreateAsync(new IdentityRole(roles));
                }
            }
        }
    }
}
