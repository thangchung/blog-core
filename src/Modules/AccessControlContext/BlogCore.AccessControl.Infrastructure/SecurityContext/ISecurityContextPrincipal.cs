using BlogCore.Core;
using System.Security.Claims;

namespace BlogCore.AccessControl.Infrastructure.SecurityContext
{
    public interface ISecurityContextPrincipal
    {
        ClaimsPrincipal Principal { set; }
        void SetBlog(EntityBase blog);
    }
}