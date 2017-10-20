using BlogCore.Core;
using System;

namespace BlogCore.PostContext.Core.Domain
{
    public class PostCreated : IDomainEvent
    {
        public PostCreated(Guid id)
        {
            PostId = id;
        }

        public Guid PostId { get; private set; }
        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}