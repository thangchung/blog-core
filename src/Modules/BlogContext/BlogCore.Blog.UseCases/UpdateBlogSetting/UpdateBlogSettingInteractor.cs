using System.Threading.Tasks;
using BlogCore.Blog.Infrastructure;
using BlogCore.Core;
using MediatR;
using BlogCore.Infrastructure.EfCore;

namespace BlogCore.Blog.UseCases.UpdateBlogSetting
{
    public class UpdateBlogSettingInteractor : IAsyncRequestHandler<UpdateBlogSettingRequest, UpdateBlogSettingResponse>
    {
        private readonly IEfRepository<BlogDbContext, Domain.Blog> _blogRepository;
        private readonly IMediator _mediator;

        public UpdateBlogSettingInteractor(IEfRepository<BlogDbContext, Domain.Blog> blogRepository, IMediator mediator)
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
                Domain.Blog.CreateBlogSettingInstane(
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