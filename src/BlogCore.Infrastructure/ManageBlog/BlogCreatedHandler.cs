using BlogCore.Core.ManageBlog;
using MediatR;

namespace BlogCore.Infrastructure.ManageBlog
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