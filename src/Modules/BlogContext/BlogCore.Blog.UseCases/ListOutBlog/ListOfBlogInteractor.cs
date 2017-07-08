using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.AccessControl.Domain.SecurityContext;
using BlogCore.Blog.Infrastructure;
using BlogCore.Core;
using MediatR;

namespace BlogCore.Blog.UseCases.ListOutBlog
{
    public class BlogInteractor : IAsyncRequestHandler<ListOfBlogRequest, IEnumerable<ListOfBlogResponse>>
    {
        private readonly IRepository<BlogDbContext, Domain.Blog> _blogRepo;
        private readonly ISecurityContext _securityContext;

        public BlogInteractor(IRepository<BlogDbContext, Domain.Blog> blogRepo, ISecurityContext securityContext)
        {
            _blogRepo = blogRepo;
            _securityContext = securityContext;
        }

        public async Task<IEnumerable<ListOfBlogResponse>> Handle(ListOfBlogRequest request)
        {
            var blogs = await _blogRepo.ListAsync();
            var responses = blogs
                .Where(x => x.OwnerEmail == _securityContext.GetCurrentEmail())
                .Select(x => new ListOfBlogResponse(
                    x.Id,
                    x.Title,
                    x.Description,
                    x.ImageFilePath,
                    x.Theme
                ));

            return responses;
        }
    }
}