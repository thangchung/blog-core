using System;
using NetCoreKit.Domain;

namespace BlogCore.Modules.BlogContext.Domain
{
    public class BlogStatusChanged : IEvent
    {
        public BlogStatusChanged(Guid id, BlogStatus status)
        {
            BlogId = id;
            Status = status;
        }

        public Guid BlogId { get; }
        public BlogStatus Status { get; }
        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}