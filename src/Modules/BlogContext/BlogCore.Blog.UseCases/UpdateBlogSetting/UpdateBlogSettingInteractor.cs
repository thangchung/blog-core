using System.Threading.Tasks;
using BlogCore.Blog.Infrastructure;
using BlogCore.Core;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace BlogCore.Blog.UseCases.UpdateBlogSetting
{
    public class UpdateBlogSettingInteractor : IInputBoundary<UpdateBlogSettingRequest, UpdateBlogSettingResponse>
    {
        private readonly BlogDbContext _dbContext;
        private readonly IMediator _mediator;

        public UpdateBlogSettingInteractor(BlogDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<UpdateBlogSettingResponse> Handle(UpdateBlogSettingRequest request)
        {
            var dbSet = _dbContext.Set<Domain.Blog>().Include(x => x.BlogSetting);
            var blog = await dbSet.FirstOrDefaultAsync(x => x.Id == request.BlogId);
            if(blog == null)
            {
                throw new CoreException($"Blog #[{request.BlogId}] is null.");
            }

            blog.ChangeSetting(
                Domain.Blog.CreateBlogSettingInstane(
                    request.PostsPerPage, 
                    request.DaysToComment, 
                    request.ModerateComments));

            _dbContext.Entry(blog).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            // raise events
            foreach (var @event in blog.GetEvents())
                await _mediator.Publish(@event);

            return new UpdateBlogSettingResponse();
        }
    }
}