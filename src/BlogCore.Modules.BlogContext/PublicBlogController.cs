using BlogCore.Shared.v1;
using BlogCore.Shared.v1.Blog;
using BlogCore.Shared.v1.Guard;
using BlogCore.Shared.v1.Usecase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

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

        [HttpGet("{blogId}/info")]
        public async Task<ActionResult<ProtoResultModel<GetBlogInfoResponse>>> GetBlogInfo(Guid blogId)
        {
            
            var useCase = _serviceProvider.GetService<IUseCase<GetBlogInfoRequest, GetBlogInfoResponse>>().NotNull();
            var response = await useCase.ExecuteAsync(new GetBlogInfoRequest
            {
                BlogId = blogId.ToString()
            });
            return new OkObjectResult(new ProtoResultModel<GetBlogInfoResponse>(response));
        }
    }
}
