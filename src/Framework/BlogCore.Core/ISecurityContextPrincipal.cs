using System.Security.Claims;

namespace BlogCore.Core
{
    public interface ISecurityContextPrincipal
    {
        ClaimsIdentity Claims { set; }
        void SetBlog(EntityBase blog);
    }
}