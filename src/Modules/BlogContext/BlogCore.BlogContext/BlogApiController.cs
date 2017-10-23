using BlogCore.BlogContext.UseCases.Crud;
using BlogCore.BlogContext.UseCases.ListOutBlogByOwner;
using BlogCore.BlogContext.UseCases.UpdateBlogSetting;
using BlogCore.Core;
using BlogCore.Infrastructure.AspNetCore;
using BlogCore.Infrastructure.UseCase;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogCore.BlogContext
{
    [Route("api/blogs")]
    public class BlogApiController : AuthorizedController
    {
        private readonly IMediator _eventAggregator;
        private readonly IAsyncUseCaseRequestHandler<CreateBlogRequest, CreateBlogResponse> _createItemHandler;
        private readonly IAsyncUseCaseRequestHandler<UpdateBlogRequest, UpdateBlogResponse> _updateItemHandler;
        private readonly IAsyncUseCaseRequestHandler<DeleteBlogRequest, DeleteBlogResponse> _deleteItemHandler;

        public BlogApiController(
            IMediator eventAggregator,
            IAsyncUseCaseRequestHandler<CreateBlogRequest, CreateBlogResponse> createItemHandler,
            IAsyncUseCaseRequestHandler<UpdateBlogRequest, UpdateBlogResponse> updateItemHandler,
            IAsyncUseCaseRequestHandler<DeleteBlogRequest, DeleteBlogResponse> deleteItemHandler
            )
        {
            _eventAggregator = eventAggregator;
            _createItemHandler = createItemHandler;
            _updateItemHandler = updateItemHandler;
            _deleteItemHandler = deleteItemHandler;
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("owner")]
        public async Task<PaginatedItem<ListOutBlogByOwnerResponse>> GetOwner()
        {
            return await _eventAggregator.Send(new ListOutBlogByOwnerRequest());
        }

        // [Authorize(Roles = "Admin, User")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<CreateBlogResponse> Post([FromBody] CreateBlogRequest request)
        {
            return await _createItemHandler.Process(request);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut("user/blog/{blogId:guid}/setting")]
        public async Task UpdateSetting(Guid blogId, [FromBody] UpdateBlogSettingRequest inputModel)
        {
            inputModel.BlogId = blogId;
            await _eventAggregator.Send(inputModel);
        }

        // [Authorize(Roles = "Admin, User")]
        [AllowAnonymous]
        [HttpPut]
        public async Task<UpdateBlogResponse> Put([FromBody] UpdateBlogRequest request)
        {
            return await _updateItemHandler.Process(request);
        }

        // [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        [HttpDelete]
        public async Task<DeleteBlogResponse> Delete([FromBody] DeleteBlogRequest request)
        {
            return await _deleteItemHandler.Process(request);
        }
    }
}