using System;
using BlogCore.Core;

namespace BlogCore.Blog.Domain
{
    public class BlogCreated : IDomainEvent
    {
        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}