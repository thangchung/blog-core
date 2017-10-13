using System;

namespace BlogCore.Core
{
    public interface ISecurityContext
    {
        bool HasClaims();
        bool IsAdmin();
        Guid GetCurrentUserId();
        string GetCurrentUserName();
        string GetCurrentEmail();
        string GetIndentityProvider();
        Guid GetBlogId();
    }
}