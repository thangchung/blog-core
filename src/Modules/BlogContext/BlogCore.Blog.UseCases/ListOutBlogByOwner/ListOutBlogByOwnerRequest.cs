using BlogCore.Core;
using MediatR;
using BlogCore.Infrastructure.EfCore;

namespace BlogCore.Blog.UseCases.ListOutBlogByOwner
{
    public class ListOutBlogByOwnerRequest : IMessage, IRequest<PaginatedItem<ListOutBlogByOwnerResponse>>
    {
    }
}