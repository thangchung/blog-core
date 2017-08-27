using BlogCore.AccessControlContext.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BlogCore.AccessControlContext.Infrastructure
{
    public class ExtendedUserStore : UserStore<AppUser>
    {
        public ExtendedUserStore(IdentityServerDbContext context, IdentityErrorDescriber describer = null) 
            : base(context, describer)
        {
        }
    }
}