using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace BlogCore.Core.BlogFeature
{
    public class BlogInteractor : IAsyncRequestHandler<ListOfBlogRequestMsg, IEnumerable<ListOfBlogResponseMsg>>
    {
        private readonly IRepository<Blog> _blogRepo;
        private readonly IValidator<ListOfBlogRequestMsg> _listOfBlogValidator;

        public BlogInteractor(IRepository<Blog> blogRepo, IValidator<ListOfBlogRequestMsg> listOfBlogValidator)
        {
            _blogRepo = blogRepo;
            _listOfBlogValidator = listOfBlogValidator;
        }

        public Task<IEnumerable<ListOfBlogResponseMsg>> Handle(ListOfBlogRequestMsg request)
        {
            var validationResult = _listOfBlogValidator.Validate(request);
            if (validationResult.IsValid == false)
            {
                // TODO: only mutation state need validation, here only for demo
                var list = new List<ListOfBlogResponseMsg> {new ListOfBlogResponseMsg(validationResult, "", "", "")};
                return Task.FromResult(list.AsEnumerable());
            }

            var blogs = _blogRepo.List();
            var responses = blogs.Select(x => new ListOfBlogResponseMsg(
                validationResult,
                x.Title,
                x.Description,
                x.Image
            ));

            // TODO: re-visit it later
            return Task.FromResult(responses);
        }
    }
}