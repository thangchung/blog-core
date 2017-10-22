using BlogCore.Core;
using BlogCore.Infrastructure.UseCase;

namespace BlogCore.BlogContext.UseCases.GetBlogsByUserName
{
    public class GetBlogsByUserNameRequest : IRequest<PaginatedItem<GetBlogsByUserNameResponse>>
    {
        public int Page { get; set; }
        public string UserName { get; set; }
    }
}
