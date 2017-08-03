using BlogCore.Core;
using BlogCore.Core.Helpers;
using BlogCore.Infrastructure.AspNetCore;
using BlogCore.Post.Domain;
using BlogCore.Post.UseCases.ListOutPostByBlog;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogCore.Post.UseCases
{
    [Route("api/posts")]
    public class PostApiController : AuthorizedController
    {
        private readonly IMediator _eventAggregator;

        public PostApiController(IMediator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        [AllowAnonymous]
        [HttpGet("blog/{blogId}")]
        public async Task<PaginatedItem<ListOutPostByBlogResponse>> GetForBlog(Guid blogId, [FromQuery] int page)
        {
            if (page <= 0) page = 1;
            return await _eventAggregator.Send(new ListOutPostByBlogRequest(blogId, page));
        }

        [HttpGet]
        public string[] Get()
        {
            return new[] {"All"};
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public Post.Domain.Post Get(int id)
        {
            return BlogCore.Post.Domain.Post.CreateInstance(
                new BlogId(IdHelper.GenerateId("34C96712-2CDF-4E79-9E2F-768CB68DD552")), 
                "sample title", 
                "sample excerpt", 
                "sample body", 
                new AuthorId(IdHelper.GenerateId("4B5F26CE-DF97-494C-B747-121D215847D8")));
        }

        [HttpPost]
        public string Post()
        {
            return "Post";
        }

        [HttpPut]
        public string Put()
        {
            return "Put";
        }

        [HttpDelete]
        public string Delete(int id)
        {
            return $"Delete {id}";
        }

        [HttpGet("{postId}/comments")]
        public string GetComments(int postId)
        {
            return "Get comments";
        }

        [HttpPost("{postId}/comments")]
        public string AddComment(int postId)
        {
            return "Add comment";
        }

        [HttpPut("{postId}/comments")]
        public string UpdateComment(int postId)
        {
            return "Update comment";
        }

        [HttpDelete("{postId}/comments/{commentId}")]
        public string DeleteComment(int postId, int commentId)
        {
            return "Delete comment";
        }
    }
}