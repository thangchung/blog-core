using System.Threading.Tasks;
using BlogCore.Core;
using MediatR;

namespace BlogCore.Blog.UseCases.GetBlog
{
    public class GetBlogInteractor : IAsyncRequestHandler<GetBlogRequest, GetBlogResponse>
    {
        private readonly IRepository<Domain.Blog> _blogRepo;

        public GetBlogInteractor(IRepository<Domain.Blog> blogRepo)
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
                blog.Theme,
                blog.ImageFilePath);
        }
    }
}