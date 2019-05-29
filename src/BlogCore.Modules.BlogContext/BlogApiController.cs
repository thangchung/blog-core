using BlogCore.DataContracts.Blog;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BlogCore.Modules.BlogContext
{
    [Route("api/blogs")]
    [ApiController]
    public class BlogApiController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<BlogDto>> Get()
        {
            var blogs = new List<BlogDto> {
                new BlogDto {
                    Id = Guid.NewGuid().ToString(),
                    Title = "My blog",
                    Description = "This is my blog",
                    Image = "/images/my-blog.png",
                    Theme = 1
                }
            };
            return Ok(blogs);
        }
    }
}
