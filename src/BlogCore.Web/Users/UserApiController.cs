using System.Collections.Generic;
using BlogCore.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Web.Users
{
    [Authorize]
    [Route("api/users")]
    public class UserApiController : Controller
    {
        private readonly ISecurityContext _securityContext;

        public UserApiController(ISecurityContext securityContext)
        {
            _securityContext = securityContext;
        }

        [HttpGet("currentInfo")]
        [AllowAnonymous]
        public List<string> Get()
        {
            return new List<string>
            {
                $"Username: {_securityContext.GetCurrentUserName()}",
                $"Email: {_securityContext.GetCurrentEmail()}"
            };
        }
    }
}