using BlogCore.BlogContext.Core.Domain;
using MediatR;

namespace BlogCore.BlogContext.UseCases.BasicCrud
{
    public class BlogSettingChangedHandler : INotificationHandler<BlogCreated>
    {
        public void Handle(BlogCreated notification)
        {
            // TODO: send an email to notify for administrator
            // TODO: ...
        }
    }
}