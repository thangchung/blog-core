using System;
using NetCoreKit.Domain;

namespace BlogCore.Modules.BlogContext.Domain
{
    public class BlogCreated : IEvent
    {
        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}