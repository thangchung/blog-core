using BlogCore.Core;
using System;

namespace BlogCore.PostContext.Domain
{
    public class PostedCreated : IDomainEvent
    {
        public PostedCreated(Guid id)
        {
            PostId = id;
        }

        public Guid PostId { get; private set; }
        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}