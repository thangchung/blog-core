using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Core.Security;
using MediatR;

namespace BlogCore.Core.Blogs.ListOutBlogs
{
    public class BlogQueryInteractor : IAsyncRequestHandler<ListOfBlogRequestMsg, IEnumerable<ListOfBlogResponseMsg>>
    {
        private readonly IRepository<Blog> _blogRepo;
        private readonly ISecurityContext _securityContext;

        public BlogQueryInteractor(IRepository<Blog> blogRepo, ISecurityContext securityContext)
        {
            _blogRepo = blogRepo;
            _securityContext = securityContext;
        }

        public async Task<IEnumerable<ListOfBlogResponseMsg>> Handle(ListOfBlogRequestMsg request)
        {
            var blogs = await _blogRepo.ListAsync();
            var responses = blogs
                .Where(x => x.OwnerEmail == _securityContext.GetCurrentEmail())
                .Select(x => new ListOfBlogResponseMsg(
                    x.Id,
                    x.Title,
                    x.Description,
                    x.Image,
                    x.Theme
                ));

            return responses;
        }
    }
}