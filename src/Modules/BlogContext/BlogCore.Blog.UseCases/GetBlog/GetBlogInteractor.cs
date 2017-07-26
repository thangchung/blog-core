using System.Threading.Tasks;
using BlogCore.Blog.Infrastructure;
using MediatR;
using BlogCore.Infrastructure.EfCore;

namespace BlogCore.Blog.UseCases.GetBlog
{
    public class GetBlogInteractor : IAsyncRequestHandler<GetBlogRequest, GetBlogResponse>
    {
        private readonly IEfRepository<BlogDbContext, Domain.Blog> _blogRepo;

        public GetBlogInteractor(IEfRepository<BlogDbContext, Domain.Blog> blogRepo)
        {
            _blogRepo = blogRepo;
        }

        public async Task<GetBlogResponse> Handle(GetBlogRequest message)
        {
            var blog = await _blogRepo.GetByIdAsync(message.Id);
            return new GetBlogResponse(
                blog.Id,
                blog.Title,
                blog.Description,
                (int)blog.Theme,
                blog.ImageFilePath);
        }
    }
}