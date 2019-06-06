using BlogCore.Shared.v1.Blog;
using System.Collections.Generic;

namespace BlogCore.Hosts.Web.Client
{
    public class AppState
    {
        public List<BlogDto> Blogs { get; set; } = new List<BlogDto>();
    }
}
