using System;

namespace BlogCore.Core.PostContext
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