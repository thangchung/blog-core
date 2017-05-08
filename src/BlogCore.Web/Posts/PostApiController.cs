using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Web.Posts
{
    [Route("api/posts")]
    public class PostApiController : Controller
    {
        [HttpGet("{id}"), /*AllowAnonymous*/]
        public string Get(int id)
        {
            return $"Post {id}";
        }
    }
}