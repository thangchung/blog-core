using BlogCore.Core;
using MediatR;

namespace BlogCore.BlogContext.UseCases.ListOutBlog
{
    public class ListOutBlogRequest : IMessage, IRequest<PaginatedItem<ListOutBlogResponse>>
    {
        public ListOutBlogRequest(int currentPage)
        {
            CurrentPage = currentPage;
        }

        public int CurrentPage { get; private set; }
    }
}