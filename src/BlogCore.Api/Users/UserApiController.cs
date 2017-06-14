using System.Collections.Generic;
using BlogCore.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Api.Users
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

        [HttpGet("settings"), AllowAnonymous]
        public List<string> Get()
        {
            return new List<string>
            {
                $"Username: {_securityContext.GetCurrentUserName()}",
                $"Email: {_securityContext.GetCurrentEmail()}"
            };
        }

        [HttpPut("settings")]
        public string Put(int id)
        {
            return "Update settings.";
        }

        [HttpPut("{id}/disable")]
        public string DisableProfile(int id)
        {
            return "Disable profile";
        }

        [HttpGet("{id}/photo")]
        public string GetProfilePhoto(int id)
        {
            return "Get profile";
        }

        [HttpPut("{id}/photo")]
        public string ChangeProfilePhoto(int id)
        {
            return "Change profile photo";
        }

        [HttpPost]
        public string Register()
        {
            return "Register";
        }
    }
}