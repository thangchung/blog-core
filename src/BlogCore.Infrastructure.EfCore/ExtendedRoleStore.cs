using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BlogCore.Infrastructure.EfCore
{
    public class ExtendedRoleStore : RoleStore<IdentityRole>
    {
        public ExtendedRoleStore(BlogCoreDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }
    }
}