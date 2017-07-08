using System;
using System.Threading.Tasks;
using BlogCore.AccessControl.Domain;
using BlogCore.Core;
using FluentValidation;
using MediatR;

namespace BlogCore.Blog.UseCases.CreateBlog
{
    public class CreateBlogInteractor : IInputBoundary<CreateBlogRequestMsg, CreateBlogResponse>
    {
        private readonly IRepository<Domain.Blog> _blogRepo;
        private readonly IValidator<CreateBlogRequestMsg> _createBlogValidator;
        private readonly ISecurityContext _securityContext;
        private readonly IMediator _mediator;

        public CreateBlogInteractor(
            IRepository<Domain.Blog> blogRepo, 
            IValidator<CreateBlogRequestMsg> createBlogValidator,
            ISecurityContext securityContext,
            IMediator mediator)
        {
            _blogRepo = blogRepo;
            _createBlogValidator = createBlogValidator;
            _securityContext = securityContext;
            _mediator = mediator;
        }

        public async Task<CreateBlogResponse> Handle(CreateBlogRequestMsg message)
        {
            if (_securityContext.HasPrincipal() == false)
            {
                throw new ViolateSecurityException("Invalid Access.");
            }

            var validationResult = _createBlogValidator.Validate(message);
            if (validationResult.IsValid == false)
                return await Task.FromResult(new CreateBlogResponse(Guid.Empty, validationResult));

            var blog = new Domain.Blog(message.Title, _securityContext.GetCurrentEmail());
            blog.UpdateDescription(message.Description);
            blog.UpdateBlogSetting(
                new Domain.BlogSetting(Guid.NewGuid(), message.PostsPerPage, message.DaysToComment, message.ModerateComments));
            
            var blogCreated = await _blogRepo.AddAsync(blog);

            // raise events
            foreach (var @event in blog.GetEvents())
            {
                await _mediator.Publish(@event);
            }

            return await Task.FromResult(new CreateBlogResponse(blogCreated.Id, validationResult));
        }
    }
}