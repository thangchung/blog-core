using System;
using System.Threading.Tasks;
using BlogCore.Post.Presenters.ListOutPostByBlog;
using BlogCore.Post.UseCases.ListOutPostByBlog;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Api
{
    [Route("api/posts")]
    public class PostApiController : Controller
    {
        private readonly IMediator _eventAggregator;
        private readonly ListOutPostByBlogPresenter _listOutPostByBlogPresenter;

        public PostApiController(
            IMediator eventAggregator, 
            ListOutPostByBlogPresenter listOutPostByBlogPresenter)
        {
            _eventAggregator = eventAggregator;
            _listOutPostByBlogPresenter = listOutPostByBlogPresenter;
        }

        [HttpGet]
        public string[] Get()
        {
            return new[] {"All"};
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"Get {id}";
        }

        [HttpGet("blog/{blogId}/{page}")]
        public async Task<ListOfPostByBlogViewModel> GetForBlog(Guid blogId, int page)
        {
            var responses = await _eventAggregator.Send(new ListOutPostByBlogRequest(blogId, page));
            return await _listOutPostByBlogPresenter.TransformAsync(responses);
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