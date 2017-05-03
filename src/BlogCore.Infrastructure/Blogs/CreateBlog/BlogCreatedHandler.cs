using BlogCore.Core.Blogs.CreateBlog;
using MediatR;

namespace BlogCore.Infrastructure.Blogs.CreateBlog
{
    public class BlogCreatedHandler : INotificationHandler<BlogCreatedEvent>
    {
        public void Handle(BlogCreatedEvent notification)
        {
            // TODO: send an email to notify for administrator
            // TODO: ...
        }
    }
}