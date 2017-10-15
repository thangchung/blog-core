using System;
using BlogCore.Core;

namespace BlogCore.BlogContext.Core.Domain
{
    public class BlogCreated : IDomainEvent
    {
        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}