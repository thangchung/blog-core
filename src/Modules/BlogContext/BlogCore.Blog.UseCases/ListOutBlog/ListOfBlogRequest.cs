using System.Collections.Generic;
using BlogCore.Core;
using MediatR;

namespace BlogCore.Blog.UseCases.ListOutBlog
{
    public class ListOfBlogRequest : IMesssage, IRequest<IEnumerable<ListOfBlogResponse>>
    {
        // TODO: re-visit for paging info later         
    }
}