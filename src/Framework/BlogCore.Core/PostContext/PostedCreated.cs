using System;

namespace BlogCore.Core.PostContext
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