using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplicationLoginRegister.Models;

namespace WebApplicationLoginRegister.Contexts
{
    public class AppDBContext : IdentityDbContext<AppUser>
    {
        public AppDBContext(DbContextOptions option) : base(option) 
        {
            
        }
    }
}
