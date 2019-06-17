using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Modules.AccessControlContext
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("myuser")]
        public bool GetUser()
        {
            return User.Identity.IsAuthenticated;
        }
    }
}
