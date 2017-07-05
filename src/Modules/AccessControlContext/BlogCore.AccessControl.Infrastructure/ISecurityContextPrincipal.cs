using System.Security.Claims;

namespace BlogCore.AccessControl.Infrastructure
{
    public interface ISecurityContextPrincipal
    {
        ClaimsPrincipal Principal { set; }
        void SetBlog(Blog.Domain.Blog blog);
    }
}