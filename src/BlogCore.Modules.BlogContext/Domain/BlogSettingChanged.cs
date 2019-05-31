using System;
using NetCoreKit.Domain;

namespace BlogCore.Modules.BlogContext.Domain
{
    public class BlogSettingChanged : IEvent
    {
        public BlogSettingChanged(Guid oldBlogSettingId)
        {
            OldBlogSettingId = oldBlogSettingId;
        }

        public Guid OldBlogSettingId { get; private set; }
        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}