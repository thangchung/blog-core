using NetCoreKit.Domain;
using System;

namespace BlogCore.Modules.PostContext.Domain
{
    public class PostCreated : IEvent
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