using BlogCore.Shared;
using BlogCore.Shared.v1;
using BlogCore.Shared.v1.Blog;
using BlogCore.Shared.v1.Guard;
using BlogCore.Shared.v1.Presenter;
using BlogCore.Shared.v1.Usecase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NetCoreKit.Domain;
using System;
using System.Threading.Tasks;

namespace BlogCore.Modules.BlogContext
{
    [Authorize]
    [Route("api/blogs")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        public BlogController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpGet("owner")]
        public Task<ActionResult<PaginatedItem<GetBlogByUserResponse>>> GetOwner()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<ActionResult<ProtoResultModel<PaginatedBlogResponse>>> RetrieveBlogs([FromQuery] int page = 1)
        {
            var useCase = _serviceProvider.GetService<IUseCase<RetrieveBlogsRequest, PaginatedBlogResponse>>().NotNull();
            var presenter = _serviceProvider.GetService<IApiPresenter<PaginatedBlogResponse>>().NotNull();

            var response = await useCase.ExecuteAsync(new RetrieveBlogsRequest {
                CurrentPage = page
            });

            return presenter.Handle(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<RetrieveBlogResponse>> Get(Guid id)
        {
            var response = new RetrieveBlogResponse
            {
                Blog = new BlogDto
                {
                    Id = id.ToString(),
                    Title = "My blog",
                    Description = "This is my blog",
                    Image = "/images/my-blog.png",
                    Theme = 1
                }
            };

            return Ok(await Task.FromResult(response.SerializeProtobufToJson()));
        }

        [HttpGet("username/{username:required}")]
        public async Task<ActionResult<ProtoResultModel<PaginatedBlogResponse>>> GetBlogByUserName(string username, [FromQuery]int page = 1)
        {
            var useCase = _serviceProvider.GetService<IUseCase<GetMyBlogsRequest, PaginatedBlogResponse>>().NotNull();
            var presenter = _serviceProvider.GetService<IApiPresenter<PaginatedBlogResponse>>().NotNull();

            var response = await useCase.ExecuteAsync(new GetMyBlogsRequest
            {
                Page = page,
                Username = username
            });

            return presenter.Handle(response);
        }

        [HttpPost]
        public async Task<ActionResult<CreateBlogResponse>> CreateBlog([FromBody] CreateBlogRequest request)
        {
            var useCase = _serviceProvider.GetService<IUseCase<CreateBlogRequest, CreateBlogResponse>>().NotNull();
            var presenter = _serviceProvider.GetService<IApiPresenter<CreateBlogResponse>>().NotNull();

            var response = await useCase.ExecuteAsync(request);
            return presenter.Handle(response);
        }

        [HttpPut("{blogId:guid}/users/{userId:guid}/setting")]
        public Task<ActionResult<UpdateBlogSettingResponse>> UpdateBlogSetting(Guid blogId, Guid userId, [FromBody] UpdateBlogSettingRequest inputModel)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id:guid}")]
        public Task<ActionResult<UpdateBlogResponse>> Put(Guid id, [FromBody] UpdateBlogRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:guid}")]
        public Task<ActionResult<DeleteBlogResponse>> Delete(Guid id, [FromBody] DeleteBlogRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
