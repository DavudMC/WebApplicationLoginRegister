using Microsoft.AspNetCore.Identity;

namespace WebApplicationLoginRegister.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
