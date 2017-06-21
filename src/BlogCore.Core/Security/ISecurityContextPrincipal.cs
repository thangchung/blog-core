using System.Security.Claims;
using BlogCore.Core.Blogs;

namespace BlogCore.Core.Security
{
    public interface ISecurityContextPrincipal
    {
        ClaimsPrincipal Principal { set; }
        void SetBlog(Blog blog);
    }
}