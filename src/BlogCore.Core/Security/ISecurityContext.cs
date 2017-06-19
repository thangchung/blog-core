using System;

namespace BlogCore.Core.Security
{
    public interface ISecurityContext
    {
        bool HasPrincipal();
        bool IsAdmin();
        Guid GetCurrentUserId();
        string GetCurrentUserName();
        string GetCurrentEmail();
        string GetIndentityProvider();
    }
}