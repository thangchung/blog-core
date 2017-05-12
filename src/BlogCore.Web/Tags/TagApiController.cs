using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Web.Tags
{
    [Route("api/tags")]
    public class TagApiController : Controller
    {
        [HttpGet]
        public string[] Get()
        {
            return new[] { "All" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"Get {id}";
        }

        [HttpPost]
        public string Post()
        {
            return "Post";
        }

        [HttpPut]
        public string Put()
        {
            return "Put";
        }

        [HttpDelete]
        public string Delete(int id)
        {
            return $"Delete {id}";
        }
    }
}