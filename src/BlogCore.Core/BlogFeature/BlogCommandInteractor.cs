using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace BlogCore.Core.BlogFeature
{
    public class BlogCommandInteractor : IAsyncRequestHandler<CreateBlogRequestMsg, CreateBlogResponseMsg>
    {
        private readonly IRepository<Blog> _blogRepo;
        private readonly IValidator<CreateBlogRequestMsg> _createBlogValidator;

        public BlogCommandInteractor(IRepository<Blog> blogRepo, IValidator<CreateBlogRequestMsg> createBlogValidator)
        {
            _blogRepo = blogRepo;
            _createBlogValidator = createBlogValidator;
        }

        public Task<CreateBlogResponseMsg> Handle(CreateBlogRequestMsg message)
        {
            var validationResult = _createBlogValidator.Validate(message);
            if (validationResult.IsValid == false)
                return Task.FromResult(new CreateBlogResponseMsg(null, validationResult));
            var blog = new Blog
            {
                Title = message.Title,
                Description = message.Description,
                Theme = message.Theme,
                PostsPerPage = message.PostsPerPage,
                DaysToComment = message.DaysToComment,
                ModerateComments = message.ModerateComments
            };
            var blogCreated = _blogRepo.Add(blog);
            return Task.FromResult(new CreateBlogResponseMsg(blogCreated.Id, validationResult));
        }
    }
}