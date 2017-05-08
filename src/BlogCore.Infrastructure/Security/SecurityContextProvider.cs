using System;
using System.Security.Claims;
using BlogCore.Infrastructure.Extensions;

namespace BlogCore.Infrastructure.Security
{
    public class SecurityContextProvider : ISecurityContext, ISecurityContextPrincipal
    {
        private const string Email = "email";
        private const string UserId = "sub";
        private const string UserName = "name";
        private const string IdentityProvider = "idp";

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

        public ClaimsPrincipal Principal { get; set; }

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