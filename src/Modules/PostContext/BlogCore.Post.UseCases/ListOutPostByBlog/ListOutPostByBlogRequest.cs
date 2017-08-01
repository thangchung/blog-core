using System;
using BlogCore.Core;
using MediatR;

namespace BlogCore.Post.UseCases.ListOutPostByBlog
{
    public class ListOutPostByBlogRequest : IMessage, IRequest<PaginatedItem<ListOutPostByBlogResponse>>
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