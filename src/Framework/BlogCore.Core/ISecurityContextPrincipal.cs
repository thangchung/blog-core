using System.Security.Claims;

namespace BlogCore.Core
{
    public interface ISecurityContextPrincipal
    {
        ClaimsPrincipal Principal { set; }
        void SetBlog(EntityBase blog);
    }
}