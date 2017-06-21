using System;
using MediatR;

namespace BlogCore.Core.Blogs.GetBlog
{
    public class GetBlogRequestMsg : IMesssage, IRequest<GetBlogResponseMsg>
    {
        public GetBlogRequestMsg(Guid id)
        {
            Id = id;            
        }

        public Guid Id { get; }
    }
}