using System.Collections.Generic;
using BlogCore.Core;
using MediatR;

namespace BlogCore.Post.UseCases.ListOutPostByBlog
{
    public class ListOutPostsByBlogRequest : IMesssage, IRequest<IEnumerable<ListOutPostByBlogResponse>>
    {
            
    }
}