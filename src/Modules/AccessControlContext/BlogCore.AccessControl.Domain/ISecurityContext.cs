using System;

namespace BlogCore.AccessControl.Domain
{
    public interface ISecurityContext
    {
        bool HasPrincipal();
        bool IsAdmin();
        Guid GetCurrentUserId();
        string GetCurrentUserName();
        string GetCurrentEmail();
        string GetIndentityProvider();
        Guid GetBlogId();
    }
}