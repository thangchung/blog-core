using System;
using System.Security.Claims;
using BlogCore.Infrastructure.Extensions;
using BlogCore.Security.Domain;

namespace BlogCore.Security.Infrastructure
{
    public class SecurityContextProvider : ISecurityContext, ISecurityContextPrincipal
    {
        private const string Email = "email";
        private const string UserId = "sub";
        private const string UserName = "name";
        private const string IdentityProvider = "idp";
        private const string Role = "role";
        private Blog.Domain.Blog _blog;

        public bool HasPrincipal()
        {
            return Principal != null;
        }

        public Guid GetCurrentUserId()
        {
            return FindFirstValue(UserId).ConvertTo<Guid>();
        }

        public string GetCurrentUserName()
        {
            return FindFirstValue(UserName);
        }

        public string GetCurrentEmail()
        {
            return FindFirstValue(Email);
        }

        public string GetIndentityProvider()
        {
            return FindFirstValue(IdentityProvider);
        }

        public Guid GetBlogId()
        {
            return _blog.Id;
        }

        public bool IsAdmin()
        {
            return FindFirstValue(Role).ToLowerInvariant() == "admin";
        }

        public ClaimsPrincipal Principal { get; set; }

        public void SetBlog(Blog.Domain.Blog blog)
        {
            _blog = blog;
        }

        private string FindFirstValue(string claimType)
        {
            if (Principal == null)
                throw new ViolateSecurityException("Principal has not been initialized.");

            var claim = Principal.FindFirst(claimType);
            if (claim == null)
                throw new ViolateSecurityException($"Claim '{claimType}' was not found.");

            return claim.Value;
        }
    }
}