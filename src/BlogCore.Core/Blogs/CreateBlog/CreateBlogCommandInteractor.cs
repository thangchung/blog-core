using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace BlogCore.Core.Blogs.CreateBlog
{
    public class CreateBlogCommandInteractor : IAsyncRequestHandler<CreateBlogRequestMsg, CreateBlogResponseMsg>
    {
        private readonly IRepository<Blog> _blogRepo;
        private readonly IValidator<CreateBlogRequestMsg> _createBlogValidator;
        private readonly IMediator _mediator;

        public CreateBlogCommandInteractor(IRepository<Blog> blogRepo, IValidator<CreateBlogRequestMsg> createBlogValidator, IMediator mediator)
        {
            _blogRepo = blogRepo;
            _createBlogValidator = createBlogValidator;
            _mediator = mediator;
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
            var blogCreated = _blogRepo.AddAsync(blog);

            // raise events
            foreach (var @event in blog.Events)
            {
                _mediator.Publish(@event);
            }

            return Task.FromResult(new CreateBlogResponseMsg(blogCreated.Id, validationResult));
        }
    }
}