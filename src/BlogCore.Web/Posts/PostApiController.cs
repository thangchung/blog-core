using System;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Web.Posts
{
    [Route("api/posts")]
    public class PostApiController : Controller
    {
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

        [HttpGet("user/{id}")]
        public string GetForUser(Guid userId)
        {
            return $"GetForUser {userId}";
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