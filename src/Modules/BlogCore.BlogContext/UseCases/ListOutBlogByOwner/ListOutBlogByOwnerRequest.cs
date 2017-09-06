using BlogCore.Core;
using MediatR;

namespace BlogCore.BlogContext.UseCases.ListOutBlogByOwner
{
    public class ListOutBlogByOwnerRequest : IRequest<PaginatedItem<ListOutBlogByOwnerResponse>>
    {
    }
}