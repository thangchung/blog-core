using Microsoft.AspNetCore.Mvc;
using System;

namespace BlogCore.Modules.BlogContext
{
    [Route("api/blogs")]
    [ApiController]
    public class PublicBlogController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        public PublicBlogController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}
