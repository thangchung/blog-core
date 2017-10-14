using BlogCore.Core;
using MediatR;
using System;

namespace BlogCore.PostContext.UseCases.ListOutPostByBlog
{
    public class ListOutPostByBlogRequest : BlogCore.Infrastructure.UseCase.IRequest<PaginatedItem<ListOutPostByBlogResponse>>
    // : IMessage, IRequest<PaginatedItem<ListOutPostByBlogResponse>>
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