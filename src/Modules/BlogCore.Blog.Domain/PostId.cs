using System;
using BlogCore.Core;

namespace BlogCore.Blog.Domain
{
    public class PostId : ValueObject
    {
        private PostId()
        {
        }

        public PostId(Guid postId)
        {
            Id = postId;
        }

        public Guid Id { get; private set; }
    }
}