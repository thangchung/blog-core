using System;
using MediatR;

namespace BlogCore.Core.Blogs.GetBlog
{
    public class GetBlogRequestMsg : IMesssage, IRequest<GetBlogResponseMsg>
    {
        public Guid Id { get; set; }
    }
}