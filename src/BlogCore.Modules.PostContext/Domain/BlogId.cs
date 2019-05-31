using NetCoreKit.Domain;
using System;

namespace BlogCore.Modules.PostContext.Domain
{
    public class BlogId : IdentityBase<Guid>
    {
        public BlogId(Guid blogId) : base(blogId)
        {
        }
    }
}