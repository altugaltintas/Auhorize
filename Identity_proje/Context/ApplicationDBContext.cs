using Identity_proje.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Security.Principal;

namespace Identity_proje.Context
{
    public class ApplicationDBContext:IdentityDbContext<AppUser>
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext>options):base(options)
        {
                
        }
    }
}
