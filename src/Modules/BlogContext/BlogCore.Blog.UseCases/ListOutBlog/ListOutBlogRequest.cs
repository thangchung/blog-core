using BlogCore.Core;
using MediatR;
using BlogCore.Infrastructure.EfCore;

namespace BlogCore.Blog.UseCases.ListOutBlog
{
    public class ListOutBlogRequest : IMessage, IRequest<PaginatedItem<ListOutBlogResponse>>
    {
        public ListOutBlogRequest(Criterion criterion)
        {
            Criterion = criterion;
        }

        public Criterion Criterion { get; private set; }
    }
}