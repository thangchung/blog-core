using System.Security.Claims;

namespace BlogCore.Security.Infrastructure
{
    public interface ISecurityContextPrincipal
    {
        ClaimsPrincipal Principal { set; }
        void SetBlog(Blog.Domain.Blog blog);
    }
}