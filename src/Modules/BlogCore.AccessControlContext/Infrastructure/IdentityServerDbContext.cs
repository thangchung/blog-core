using BlogCore.AccessControlContext.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.AccessControlContext.Infrastructure
{
    public class IdentityServerDbContext : IdentityDbContext<AppUser>
    {
        public IdentityServerDbContext(DbContextOptions<IdentityServerDbContext> options)
            : base(options)
        {
        }
    }
}
