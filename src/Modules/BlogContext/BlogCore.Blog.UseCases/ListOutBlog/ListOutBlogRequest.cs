using System.Collections.Generic;
using BlogCore.Core;
using MediatR;

namespace BlogCore.Blog.UseCases.ListOutBlog
{
    public class ListOutBlogRequest : IMessage, IRequest<IEnumerable<ListOutBlogResponse>>
    {
        public ListOutBlogRequest(int page)
        {
            Page = page;
        }

        public int Page { get; private set; }
    }
}