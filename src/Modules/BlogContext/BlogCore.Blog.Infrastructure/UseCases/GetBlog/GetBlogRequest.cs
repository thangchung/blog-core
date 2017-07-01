using System;
using BlogCore.Core;
using MediatR;

namespace BlogCore.Blog.Infrastructure.UseCases.GetBlog
{
    public class GetBlogRequest : IMesssage, IRequest<GetBlogResponse>
    {
        public GetBlogRequest(Guid id)
        {
            Id = id;            
        }

        public Guid Id { get; }
    }
}