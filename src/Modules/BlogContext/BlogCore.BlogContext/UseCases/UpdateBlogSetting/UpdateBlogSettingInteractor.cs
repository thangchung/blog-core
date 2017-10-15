using BlogCore.BlogContext.Infrastructure;
using BlogCore.Core;
using BlogCore.Infrastructure.EfCore;
using MediatR;
using System.Threading.Tasks;

namespace BlogCore.BlogContext.UseCases.UpdateBlogSetting
{
    public class UpdateBlogSettingInteractor : IAsyncRequestHandler<UpdateBlogSettingRequest, UpdateBlogSettingResponse>
    {
        private readonly IEfRepository<BlogDbContext, Core.Domain.Blog> _blogRepository;
        private readonly IMediator _mediator;

        public UpdateBlogSettingInteractor(IEfRepository<BlogDbContext, Core.Domain.Blog> blogRepository, IMediator mediator)
        {
            _blogRepository = blogRepository;
            _mediator = mediator;
        }

        public async Task<UpdateBlogSettingResponse> Handle(UpdateBlogSettingRequest request)
        {
            var blog = await _blogRepository.FindOneAsync(
                x => x.Id == request.BlogId,
                x => x.BlogSetting);

            if (blog == null)
            {
                throw new CoreException($"Blog #[{request.BlogId}] is null.");
            }

            blog.ChangeSetting(
                Core.Domain.Blog.CreateBlogSettingInstane(
                    request.PostsPerPage, 
                    request.DaysToComment, 
                    request.ModerateComments));

            await _blogRepository.DbContext.SaveChangesAsync();

            // raise events
            foreach (var @event in blog.GetEvents())
                await _mediator.Publish(@event);

            return new UpdateBlogSettingResponse();
        }
    }
}