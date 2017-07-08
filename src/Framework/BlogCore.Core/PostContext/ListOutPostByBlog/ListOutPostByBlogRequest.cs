using System.Collections.Generic;
using MediatR;

namespace BlogCore.Core.PostContext.ListOutPostByBlog
{
    public class ListOutPostsByBlogRequest : IMesssage, IRequest<IEnumerable<ListOutPostByBlogResponse>>
    {
            
    }
}