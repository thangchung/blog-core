using System;
using BlogCore.Core;

namespace BlogCore.Blog.Domain
{
    public class BlogStatusChangedEvent : DomainEventBase
    {
        public BlogStatusChangedEvent(Guid id, BlogStatus status)
        {
            BlogId = id;
            Status = status;
        }

        public Guid BlogId { get; }

        public BlogStatus Status { get; }    
    }
}