using BlogCore.Core;
using System;

namespace BlogCore.PostContext.Domain
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