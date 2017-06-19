using System.Security.Claims;

namespace BlogCore.Core.Security
{
    public interface ISecurityContextPrincipal
    {
        ClaimsPrincipal Principal { set; }
    }
}