using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.AccessControl.Domain.SecurityContext;
using BlogCore.Blog.Infrastructure;
using MediatR;
using BlogCore.Infrastructure.EfCore;

namespace BlogCore.Blog.UseCases.ListOutBlogByOwner
{
    public class ListOutBlogByOwnerInteractor : IAsyncRequestHandler<ListOutBlogByOwnerRequest, IEnumerable<ListOutBlogByOwnerResponse>>
    {
        private readonly IEfRepository<BlogDbContext, Domain.Blog> _blogRepo;
        private readonly ISecurityContext _securityContext;

        public ListOutBlogByOwnerInteractor(IEfRepository<BlogDbContext, Domain.Blog> blogRepo, ISecurityContext securityContext)
        {
            _blogRepo = blogRepo;
            _securityContext = securityContext;
        }

        public async Task<IEnumerable<ListOutBlogByOwnerResponse>> Handle(ListOutBlogByOwnerRequest request)
        {
            var blogs = await _blogRepo.FindAllAsync(x => x.OwnerEmail == _securityContext.GetCurrentEmail());
            var responses = blogs.Select(
                x => new ListOutBlogByOwnerResponse(
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