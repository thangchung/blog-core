using System.Collections.Generic;
using BlogCore.Core;
using MediatR;

namespace BlogCore.Blog.UseCases.ListOutBlogByOwner
{
    public class ListOutBlogByOwnerRequest : IMesssage, IRequest<IEnumerable<ListOutBlogByOwnerResponse>>
    {
        // TODO: re-visit for paging info later         
    }
}