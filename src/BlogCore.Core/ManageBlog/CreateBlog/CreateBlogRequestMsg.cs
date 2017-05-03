using MediatR;

namespace BlogCore.Core.ManageBlog.CreateBlog
{
    public class CreateBlogRequestMsg : IMesssage, IRequest<CreateBlogResponseMsg>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Theme { get; set; } = "default";
        public int PostsPerPage { get; set; } = 10;
        public int DaysToComment { get; set; } = 5;
        public bool ModerateComments { get; set; } = false;
    }
}