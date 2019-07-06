using BlogCore.Shared;
using BlogCore.Shared.v1;
using BlogCore.Shared.v1.Blog;
using BlogCore.Shared.v1.Presenter;
using BlogCore.Shared.v1.Usecase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreKit.Domain;
using System;
using System.Threading.Tasks;

namespace BlogCore.Modules.BlogContext
{
    //[Authorize]
    [Route("api/blogs")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IUseCase<RetrieveBlogsRequest, PaginatedBlogResponse> _retrieveBlogsUseCase;
        private readonly IUseCase<GetMyBlogsRequest, PaginatedBlogResponse> _getBlogByUsernameUseCase;
        private readonly IApiPresenter<PaginatedBlogResponse> _retrieveBlogsPresenter;
        public BlogController(
            IUseCase<RetrieveBlogsRequest, PaginatedBlogResponse> retrieveBlogsUseCase,
            IUseCase<GetMyBlogsRequest, PaginatedBlogResponse> getBlogByUsernameUseCase,
            IApiPresenter<PaginatedBlogResponse> retrieveBlogsPresenter
            )
        {
            _retrieveBlogsUseCase = retrieveBlogsUseCase;
            _getBlogByUsernameUseCase = getBlogByUsernameUseCase;
            _retrieveBlogsPresenter = retrieveBlogsPresenter;
        }

        [HttpGet("owner")]
        public async Task<ActionResult<PaginatedItem<GetBlogByUserResponse>>> GetOwner()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<ActionResult<ProtoResultModel<PaginatedBlogResponse>>> RetrieveBlogs([FromQuery] int page = 1)
        {
            var response = await _retrieveBlogsUseCase.ExecuteAsync(new RetrieveBlogsRequest {
                CurrentPage = page
            });
            _retrieveBlogsPresenter.Handle(response);
            return _retrieveBlogsPresenter.OkResult;
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
            var response = await _getBlogByUsernameUseCase.ExecuteAsync(new GetMyBlogsRequest
            {
                Page = page,
                Username = username
            });
            _retrieveBlogsPresenter.Handle(response);
            return _retrieveBlogsPresenter.OkResult;
        }

        [HttpPost]
        public async Task<ActionResult<CreateBlogResponse>> CreateBlog([FromBody] CreateBlogRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{blogId:guid}/users/{userId:guid}/setting")]
        public async Task<ActionResult<UpdateBlogSettingResponse>> UpdateBlogSetting(Guid blogId, Guid userId, [FromBody] UpdateBlogSettingRequest inputModel)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UpdateBlogResponse>> Put(Guid id, [FromBody] UpdateBlogRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<DeleteBlogResponse>> Delete(Guid id, [FromBody] DeleteBlogRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
