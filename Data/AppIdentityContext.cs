using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class AppIdentityContext : IdentityDbContext
    {
        
        public AppIdentityContext(DbContextOptions<AppIdentityContext> options) :
            base(options)
        { }
    }
}
