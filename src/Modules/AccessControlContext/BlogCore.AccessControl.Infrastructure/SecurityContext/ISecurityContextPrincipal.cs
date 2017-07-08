using System.Security.Claims;

namespace BlogCore.AccessControl.Infrastructure.SecurityContext
{
    public interface ISecurityContextPrincipal
    {
        ClaimsPrincipal Principal { set; }
        void SetBlog(Blog.Domain.Blog blog);
    }
}