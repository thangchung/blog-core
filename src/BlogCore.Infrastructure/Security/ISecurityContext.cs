using System;

namespace BlogCore.Infrastructure.Security
{
    public interface ISecurityContext
    {
        bool HasPrincipal();
        Guid GetCurrentUserId();
        string GetCurrentUserName();
        string GetCurrentEmail();
        string GetIndentityProvider();
    }
}