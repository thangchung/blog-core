using System;
using BlogCore.Core;
using MediatR;

namespace BlogCore.Blog.UseCases.GetBlog
{
    public class GetBlogRequest : IMessage, IRequest<GetBlogResponse>
    {
        public GetBlogRequest(Guid id)
        {
            Id = id;            
        }

        public Guid Id { get; }
    }
}