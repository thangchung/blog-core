using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogCore.AccessControl.UseCases.UpdateUserProfileSetting;
using BlogCore.Blog.Presenters.GetBlog;
using BlogCore.Blog.Presenters.ListOutBlog;
using BlogCore.Blog.Presenters.ListOutBlogByOwner;
using BlogCore.Blog.Presenters.Shared;
using BlogCore.Blog.UseCases.CreateBlog;
using BlogCore.Blog.UseCases.GetBlog;
using BlogCore.Blog.UseCases.ListOutBlog;
using BlogCore.Blog.UseCases.ListOutBlogByOwner;
using BlogCore.Blog.UseCases.UpdateBlogSetting;
using BlogCore.Core;
using BlogCore.Infrastructure.AspNetCore;
using BlogCore.Post.Presenters.ListOutPostByBlog;
using BlogCore.Post.UseCases.ListOutPostByBlog;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BlogCore.Infrastructure.EfCore;

namespace BlogCore.Api
{
    [Route("api/blogs")]
    public class BlogApiController : AuthorizedController
    {
        private readonly IMediator _eventAggregator;
        private readonly GetBlogPresenter _getBlogPresenter;
        private readonly ListOfBlogByOwnerPresenter _listOfBlogByOwnerPresenter;
        private readonly ListOfBlogPresenter _listOfBlogPresenter;
        private readonly ListOutPostByBlogPresenter _listOutPostByBlogPresenter;

        public BlogApiController(
            IMediator eventAggregator,
            ListOfBlogByOwnerPresenter listOfBlogByOwnerPresenter,
            ListOfBlogPresenter listOfBlogPresenter,
            GetBlogPresenter getBlogPresenter,
            ListOutPostByBlogPresenter listOutPostByBlogPresenter)
        {
            _eventAggregator = eventAggregator;
            _listOfBlogByOwnerPresenter = listOfBlogByOwnerPresenter;
            _listOfBlogPresenter = listOfBlogPresenter;
            _getBlogPresenter = getBlogPresenter;
            _listOutPostByBlogPresenter = listOutPostByBlogPresenter;
        }

        [Authorize("Admin")]
        [HttpGet("owner")]
        public async Task<PaginatedItem<ListOutBlogByOwnerResponse>> GetOwner()
        {
            return await _eventAggregator.Send(new ListOutBlogByOwnerRequest());
        }

        [AllowAnonymous]
        [HttpGet("paged/{page}")]
        public async Task<PaginatedItem<ListOutBlogResponse>> GetByPage(int page)
        {
            var criterion = new Criterion(page, 20);
            return await _eventAggregator.Send(new ListOutBlogRequest(criterion));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<BlogItemViewModel> Get(Guid id)
        {
            var blogResponse = await _eventAggregator.Send(new GetBlogRequest(id));
            return await _getBlogPresenter.TransformAsync(blogResponse);
        }

        [AllowAnonymous]
        [HttpGet("{blogId}/posts")]
        public async Task<ListOfPostByBlogViewModel> GetForBlog(Guid blogId, [FromQuery] int page)
        {
            var responses = await _eventAggregator.Send(new ListOutPostByBlogRequest(blogId, page));
            return await _listOutPostByBlogPresenter.TransformAsync(responses);
        }

        [Authorize("User")]
        [HttpPost]
        public async Task<CreateBlogResponse> Post([FromBody] CreateBlogRequestMsg blogRequest)
        {
            return await _eventAggregator.Send(blogRequest);
        }

        [AllowAnonymous]
        [HttpPut("setting/{userId}")]
        public async Task UpdateSetting(Guid userId, [FromBody] BlogSettingViewModel viewModel)
        {
            await _eventAggregator.Send(new UpdateUserProfileSettingRequest
            {
                UserId = userId,
                GivenName = viewModel.GivenName,
                FamilyName = viewModel.FamilyName,
                Bio = viewModel.Bio,
                Company = viewModel.Company,
                Location = viewModel.Location
            });
            await _eventAggregator.Send(new UpdateBlogSettingRequest
            {
                BlogId = viewModel.BlogId,
                DaysToComment = viewModel.DaysToComment,
                ModerateComments = viewModel.ModerateComments,
                PostsPerPage = viewModel.PostsPerPage
            });
        }

        [Authorize("User")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [Authorize("User")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class BlogSettingViewModel : IViewModel
    {
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Bio { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public Guid BlogId { get; set; }
        public int PostsPerPage { get; set; }
        public int DaysToComment { get; set; }
        public bool ModerateComments { get; set; }
    }
}