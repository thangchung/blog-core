using BlogCore.Core.BlogFeature;
using MediatR;

namespace BlogCore.Infrastructure.BlogFeature
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