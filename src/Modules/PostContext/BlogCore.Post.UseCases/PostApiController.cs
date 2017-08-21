using BlogCore.Infrastructure.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public string[] Get()
        {
            return new[] {"All"};
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