using BlogCore.AccessControl.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.AccessControl.Infrastructure
{
    public class IdentityServerDbContext : IdentityDbContext<AppUser>
    {
        public IdentityServerDbContext(DbContextOptions<IdentityServerDbContext> options)
            : base(options)
        {
        }
    }
}
