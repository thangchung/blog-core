using System;
using BlogCore.Core;

namespace BlogCore.Blog.Domain
{
    public class PostId : IdentityBase
    {
        private PostId()
        {
        }

        public PostId(Guid postId) : base(postId)
        {
            Id = postId;
        }
    }
}