using System.Security.Claims;

namespace BlogCore.Infrastructure.Security
{
    public interface ISecurityContextPrincipal
    {
        ClaimsPrincipal Principal { set; }
    }
}