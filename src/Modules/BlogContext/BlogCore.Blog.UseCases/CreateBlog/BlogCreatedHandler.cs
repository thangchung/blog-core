using BlogCore.Blog.Domain;
using MediatR;

namespace BlogCore.Blog.UseCases.CreateBlog
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