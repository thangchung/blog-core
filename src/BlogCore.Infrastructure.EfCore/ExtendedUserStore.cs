using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BlogCore.Infrastructure.EfCore
{
    public class ExtendedUserStore : UserStore<AppUser>
    {
        public ExtendedUserStore(BlogCoreDbContext context, IdentityErrorDescriber describer = null) 
            : base(context, describer)
        {
        }
    }
}