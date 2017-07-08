using System;
using System.Collections.Generic;
using BlogCore.Core;

namespace BlogCore.Blog.Domain
{
    public class PostId : ValueObjectBase
    {
        private PostId()
        {
        }

        public PostId(Guid postId)
        {
            Id = postId;
        }

        public Guid Id { get; private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}