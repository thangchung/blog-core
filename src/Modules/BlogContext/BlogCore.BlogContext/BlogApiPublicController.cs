using BlogCore.BlogContext.UseCases.BasicCrud;
using BlogCore.BlogContext.UseCases.GetBlogsByUserName;
using BlogCore.Core;
using BlogCore.Infrastructure.UseCase;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogCore.BlogContext
{
    [Route("public/api/blogs")]
    public class BlogApiPublicController : Controller
    {
        private readonly IAsyncUseCaseRequestHandler<RetrieveBlogsRequest, PaginatedItem<RetrieveBlogsResponse>> _retrieveItemsHandler;
        private readonly IAsyncUseCaseRequestHandler<RetrieveBlogRequest, RetrieveBlogResponse> _retrieveItemHandler;
        private readonly GetBlogsByUserNameInteractor _getBlogsByUserNameInteractor;

        public BlogApiPublicController(
            IAsyncUseCaseRequestHandler<RetrieveBlogsRequest, PaginatedItem<RetrieveBlogsResponse>> retrieveItemsHandler,
            IAsyncUseCaseRequestHandler<RetrieveBlogRequest, RetrieveBlogResponse> retrieveItemHandler,
            GetBlogsByUserNameInteractor getBlogsByUserNameInteractor
            )
        {
            _retrieveItemsHandler = retrieveItemsHandler;
            _retrieveItemHandler = retrieveItemHandler;
            _getBlogsByUserNameInteractor = getBlogsByUserNameInteractor;
        }

        [HttpGet("")]
        public async Task<PaginatedItem<RetrieveBlogsResponse>> GetByPage([FromQuery] int page = 1)
        {
            return await _retrieveItemsHandler.ProcessAsync(new RetrieveBlogsRequest(page));
        }

        [HttpGet("{id:guid}")]
        public async Task<RetrieveBlogResponse> Get(Guid id)
        {
            return await _retrieveItemHandler.ProcessAsync(new RetrieveBlogRequest(id));
        }

        [HttpGet("{username:required}")]
        public async Task<PaginatedItem<GetBlogsByUserNameResponse>> GetBlogByUserName(string username, [FromQuery]int page = 1)
        {
            return await _getBlogsByUserNameInteractor.ProcessAsync(new GetBlogsByUserNameRequest
            {
                Page = page,
                UserName = username
            });
        }
    }
}