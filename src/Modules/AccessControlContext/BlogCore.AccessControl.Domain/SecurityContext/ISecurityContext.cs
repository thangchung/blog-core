using System;

namespace BlogCore.AccessControl.Domain.SecurityContext
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