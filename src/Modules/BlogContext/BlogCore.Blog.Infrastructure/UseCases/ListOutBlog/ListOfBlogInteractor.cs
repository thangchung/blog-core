using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.AccessControl.Domain;
using BlogCore.Core;
using MediatR;

namespace BlogCore.Blog.Infrastructure.UseCases.ListOutBlog
{
    public class BlogInteractor : IAsyncRequestHandler<ListOfBlogRequest, IEnumerable<ListOfBlogResponse>>
    {
        private readonly IRepository<Domain.Blog> _blogRepo;
        private readonly ISecurityContext _securityContext;

        public BlogInteractor(IRepository<Domain.Blog> blogRepo, ISecurityContext securityContext)
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