using BlogCore.Core;
using BlogCore.Infrastructure.Extensions;
using System;
using System.Security.Claims;

namespace BlogCore.AccessControlContext.Infrastructure
{
    public class SecurityContextProvider : ISecurityContext, ISecurityContextPrincipal
    {
        private const string Email = "email";
        private const string UserId = "sub";
        private const string UserName = "name";
        private const string IdentityProvider = "idp";
        private const string Role = "role";
        private EntityBase _blog;

        public bool HasClaims()
        {
            return Claims != null;
        }

        public Guid GetCurrentUserId()
        {
            return Claims.FindFirst(UserId).Value.ConvertTo<Guid>();
        }

        public string GetCurrentUserName()
        {
            return Claims.FindFirst(UserName).Value;
        }

        public string GetCurrentEmail()
        {
            return Claims.FindFirst(Email).Value;
        }

        public string GetIndentityProvider()
        {
            return Claims.FindFirst(IdentityProvider).Value;
        }

        public Guid GetBlogId()
        {
            return _blog.Id;
        }

        public bool IsAdmin()
        {
            return Claims.FindFirst(Role).Value == "admin";
        }

        public ClaimsIdentity Claims { get; set; }

        public void SetBlog(EntityBase blog)
        {
            _blog = blog;
        }
    }
}