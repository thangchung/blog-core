using System.Collections.Generic;
using BlogCore.Core.Blogs.ListOutBlogs;
using MediatR;

namespace BlogCore.Core.Posts.ListOutPostsByBlog
{
    public class ListOutPostsByBlogRequest : IMesssage, IRequest<IEnumerable<ListOutPostsByBlogResponse>>
    {
            
    }
}