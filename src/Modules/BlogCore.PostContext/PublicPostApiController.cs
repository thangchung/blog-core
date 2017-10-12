using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.PostContext
{
    [Route("public/api/posts")]
    public class PublicPostApiController : Controller
    {
        private readonly IMediator _eventAggregator;

        public PublicPostApiController(IMediator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
    }
}
