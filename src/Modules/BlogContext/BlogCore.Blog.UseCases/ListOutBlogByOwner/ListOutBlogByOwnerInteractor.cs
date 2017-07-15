using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.AccessControl.Domain.SecurityContext;
using BlogCore.Blog.Infrastructure;
using BlogCore.Core;
using MediatR;

namespace BlogCore.Blog.UseCases.ListOutBlogByOwner
{
    public class ListOutBlogByOwnerInteractor : IAsyncRequestHandler<ListOutBlogByOwnerRequest, IEnumerable<ListOutBlogByOwnerResponse>>
    {
        private readonly IRepository<BlogDbContext, Domain.Blog> _blogRepo;
        private readonly ISecurityContext _securityContext;

        public ListOutBlogByOwnerInteractor(IRepository<BlogDbContext, Domain.Blog> blogRepo, ISecurityContext securityContext)
        {
            _blogRepo = blogRepo;
            _securityContext = securityContext;
        }

        public async Task<IEnumerable<ListOutBlogByOwnerResponse>> Handle(ListOutBlogByOwnerRequest request)
        {
            var blogs = await _blogRepo.ListAsync();
            var responses = blogs
                .Where(x => x.OwnerEmail == _securityContext.GetCurrentEmail())
                .Select(x => new ListOutBlogByOwnerResponse(
                    x.Id,
                    x.Title,
                    x.Description,
                    x.ImageFilePath,
                    (int)x.Theme
                ));

            return responses;
        }
    }
}