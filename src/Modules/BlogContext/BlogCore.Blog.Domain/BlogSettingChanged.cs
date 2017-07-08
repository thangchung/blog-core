using System;
using BlogCore.Core;

namespace BlogCore.Blog.Domain
{
    public class BlogSettingChanged : IDomainEvent
    {
        public BlogSettingChanged(Guid blogSettingId)
        {
            BlogSettingId = blogSettingId;
        }

        public Guid BlogSettingId { get; private set; }

        public int EventVersion { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}