using System;
using System.Collections.Generic;
using BlogCore.Core;
using MediatR;

namespace BlogCore.Post.UseCases.ListOutPostByBlog
{
    public class ListOutPostByBlogRequest : IMessage, IRequest<ListOutPostByBlogResponse>
    {
        public ListOutPostByBlogRequest(Guid blogId, int page)
        {
            BlogId = blogId;
            Page = page;
        }

        public Guid BlogId { get; private set; }
        public int Page { get; private set; }
    }
}