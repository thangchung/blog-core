using BlogCore.BlogContext.Infrastructure;
using BlogCore.Infrastructure.EfCore;
using MediatR;
using System.Threading.Tasks;

namespace BlogCore.BlogContext.UseCases.GetBlog
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