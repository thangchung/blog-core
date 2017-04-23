using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace BlogCore.Core.BlogFeature
{
    public class BlogInteractor : IAsyncRequestHandler<ListOfBlogRequestMsg, IEnumerable<ListOfBlogResponseMsg>>
    {
        private readonly IRepository<Blog> _blogRepo;

        public BlogInteractor(IRepository<Blog> blogRepo)
        {
            _blogRepo = blogRepo;
        }

        public Task<IEnumerable<ListOfBlogResponseMsg>> Handle(ListOfBlogRequestMsg message)
        {
            var blogs = _blogRepo.List();
            var responses = blogs.Select(x => new ListOfBlogResponseMsg(
                x.Title,
                x.Description,
                x.Image
            ));

            // TODO: re-visit it later
            return Task.FromResult(responses);
        }
    }
}