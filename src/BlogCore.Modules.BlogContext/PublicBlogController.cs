using BlogCore.Shared.v1;
using BlogCore.Shared.v1.Blog;
using BlogCore.Shared.v1.Common;
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

        [HttpGet("owner")]
        public Task<ActionResult<ProtoResultModel<PaginatedItemResponse>>> GetOwner()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<ActionResult<ProtoResultModel<PaginatedItemResponse>>> RetrieveBlogs([FromQuery] int page = 1)
        {
            var useCase = _serviceProvider.GetService<IUseCase<RetrieveBlogsRequest, PaginatedItemResponse>>().NotNull();
            var response = await useCase.ExecuteAsync(new RetrieveBlogsRequest
            {
                CurrentPage = page
            });
            return new OkObjectResult(new ProtoResultModel<PaginatedItemResponse>(response));
        }
    }
}
