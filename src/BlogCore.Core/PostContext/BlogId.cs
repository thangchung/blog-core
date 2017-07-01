using System;

namespace BlogCore.Core.PostContext
{
    public class BlogId : ValueObject
    {
        private BlogId()
        {
        }

        public BlogId(Guid blogId)
        {
            Id = blogId;
        }

        public Guid Id { get; private set; }
    }
}