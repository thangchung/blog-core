using System.Collections.Generic;
using System.Reactive.Linq;
using BlogCore.Core.BlogFeature;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Web.Controllers
{
    [Route("api/blogs")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public IEnumerable<Blog> Get()
        {
            return _blogService.GetBlogs().ToEnumerable();
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "blog";
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
