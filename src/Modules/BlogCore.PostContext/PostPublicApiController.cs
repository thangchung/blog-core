using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.PostContext
{
    [Route("api/public/posts")]
    public class PostPublicApiController : Controller
    {
        private readonly IMediator _eventAggregator;

        public PostPublicApiController(IMediator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
    }
}
