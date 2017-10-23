using BlogCore.Core;
using BlogCore.Infrastructure.UseCase;

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