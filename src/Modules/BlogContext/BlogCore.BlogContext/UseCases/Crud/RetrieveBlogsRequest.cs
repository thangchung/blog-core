using BlogCore.Core;
using MediatR;

namespace BlogCore.BlogContext.UseCases.Crud
{
    public class RetrieveBlogsRequest : IRequest<PaginatedItem<RetrieveBlogsResponse>>
    {
        public RetrieveBlogsRequest(int currentPage)
        {
            CurrentPage = currentPage;
        }

        public int CurrentPage { get; private set; }
    }
}