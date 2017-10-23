using BlogCore.BlogContext.UseCases.Crud;
using BlogCore.BlogContext.UseCases.GetBlogsByUserName;
using BlogCore.Core;
using BlogCore.Infrastructure.UseCase;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogCore.BlogContext
{
    [Route("public/api/blogs")]
    public class BlogApiPublicController : Controller
    {
        private readonly IMediator _eventAggregator;
        private readonly IAsyncUseCaseRequestHandler<RetrieveBlogsRequest, PaginatedItem<RetrieveBlogsResponse>> _retrieveItemsHandler;
        private readonly IAsyncUseCaseRequestHandler<RetrieveBlogRequest, RetrieveBlogResponse> _retrieveItemHandler;
        private readonly GetBlogsByUserNameInteractor _getBlogsByUserNameInteractor;

        public BlogApiPublicController(
            IMediator eventAggregator,
            IAsyncUseCaseRequestHandler<RetrieveBlogsRequest, PaginatedItem<RetrieveBlogsResponse>> retrieveItemsHandler,
            IAsyncUseCaseRequestHandler<RetrieveBlogRequest, RetrieveBlogResponse> retrieveItemHandler,
            GetBlogsByUserNameInteractor getBlogsByUserNameInteractor
            )
        {
            _eventAggregator = eventAggregator;
            _retrieveItemsHandler = retrieveItemsHandler;
            _retrieveItemHandler = retrieveItemHandler;
            _getBlogsByUserNameInteractor = getBlogsByUserNameInteractor;
        }

        [HttpGet("")]
        public async Task<PaginatedItem<RetrieveBlogsResponse>> GetByPage([FromQuery] int page)
        {
            // return await _eventAggregator.Send(new RetrieveBlogsRequest(page));
            return await _retrieveItemsHandler.Process(new RetrieveBlogsRequest(page));
        }

        [HttpGet("{id:guid}")]
        public async Task<RetrieveBlogResponse> Get(Guid id)
        {
            // return await _eventAggregator.Send(new RetrieveBlogRequest(id));
            return await _retrieveItemHandler.Process(new RetrieveBlogRequest(id));
        }

        [HttpGet("{username:required}")]
        public async Task<PaginatedItem<GetBlogsByUserNameResponse>> GetBlogByUserName(string username, [FromQuery]int page = 1)
        {
            return await _getBlogsByUserNameInteractor.Process(new GetBlogsByUserNameRequest
            {
                Page = page,
                UserName = username
            });
        }
    }
}