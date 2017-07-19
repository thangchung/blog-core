using System.Threading.Tasks;
using BlogCore.Blog.Domain;
using BlogCore.Blog.Infrastructure;
using BlogCore.Core;

namespace BlogCore.Blog.UseCases.UpdateBlogSetting
{
    public class UpdateBlogSettingInteractor : IInputBoundary<UpdateBlogSettingRequest, UpdateBlogSettingResponse>
    {
        private readonly IRepository<BlogDbContext, Domain.Blog> _blogRepo;

        public UpdateBlogSettingInteractor(IRepository<BlogDbContext, Domain.Blog> blogRepo)
        {
            _blogRepo = blogRepo;
        }

        public async Task<UpdateBlogSettingResponse> Handle(UpdateBlogSettingRequest request)
        {
            var blog = await _blogRepo.GetByIdAsync(request.BlogId);
            blog.ChangeSetting(
                new BlogSetting(
                    IdHelper.GenerateId(),
                    request.PostsPerPage,
                    request.DaysToComment,
                    request.ModerateComments));
            await _blogRepo.UpdateAsync(blog);
            return new UpdateBlogSettingResponse();
        }
    }
}