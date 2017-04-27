using System.Collections.Generic;
using System.Linq;
using BlogCore.Core.BlogFeature;

namespace BlogCore.Web.BlogFeature
{
    public class BlogPresenter
    {
        public IEnumerable<ListOfBlogViewModel> Handle(IEnumerable<ListOfBlogResponseMsg> blogResponses)
        {
            return blogResponses.Select(x => new ListOfBlogViewModel
            {
                Title = x.Title,
                Description = x.Description,
                Image = x.Image
            });
        }

        public CategoryCreatedViewModel Handle(CreateBlogResponseMsg response)
        {
            // TODO: we will send the full blog object back to client
            return new CategoryCreatedViewModel
            {
                BlogId = response.BlogId
            };
        }
    }
}