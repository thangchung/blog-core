using System;
using BlogCore.Core;

namespace BlogCore.Post.Domain
{
    public class BlogId : IdentityBase
    {
        private BlogId()
        {
        }

        public BlogId(Guid blogId) : base(blogId)
        {
        }
    }
}