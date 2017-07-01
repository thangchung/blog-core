using System;
using System.Threading.Tasks;
using BlogCore.Core;
using FluentValidation;
using MediatR;

namespace BlogCore.Blog.Infrastructure.UseCases.CreateBlog
{
    public class CreateBlogInteractor : IInputBoundary<CreateBlogRequestMsg, CreateBlogResponse>
    {
        private readonly IRepository<Domain.Blog> _blogRepo;
        private readonly IValidator<CreateBlogRequestMsg> _createBlogValidator;
        private readonly IMediator _mediator;

        public CreateBlogInteractor(IRepository<Domain.Blog> blogRepo, IValidator<CreateBlogRequestMsg> createBlogValidator, IMediator mediator)
        {
            _blogRepo = blogRepo;
            _createBlogValidator = createBlogValidator;
            _mediator = mediator;
        }

        public async Task<CreateBlogResponse> Handle(CreateBlogRequestMsg message)
        {
            var validationResult = _createBlogValidator.Validate(message);
            if (validationResult.IsValid == false)
                return await Task.FromResult(new CreateBlogResponse(Guid.Empty, validationResult));

            var blog = new Domain.Blog
            {
                Title = message.Title,
                Description = message.Description,
                Theme = message.Theme,
                PostsPerPage = message.PostsPerPage,
                DaysToComment = message.DaysToComment,
                ModerateComments = message.ModerateComments
            };
            var blogCreated = await _blogRepo.AddAsync(blog);

            // raise events
            foreach (var @event in blog.Events)
            {
                await _mediator.Publish(@event);
            }

            return await Task.FromResult(new CreateBlogResponse(blogCreated.Id, validationResult));
        }
    }
}