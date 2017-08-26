using BlogCore.BlogContext.Domain;
using BlogCore.BlogContext.Infrastructure;
using BlogCore.BlogContext.UseCases.CreateBlog;
using BlogCore.Core;
using BlogCore.Core.Helpers;
using BlogCore.Infrastructure.EfCore;
using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;

namespace BlogCore.BlogBlogContext.UseCases.CreateBlog
{
    public class CreateBlogInteractor : IAsyncRequestHandler<CreateBlogRequestMsg, CreateBlogResponse>
    {
        private readonly IEfRepository<BlogDbContext, BlogContext.Domain.Blog> _blogRepo;
        private readonly IValidator<CreateBlogRequestMsg> _createBlogValidator;
        private readonly IMediator _mediator;
        private readonly ISecurityContext _securityContext;

        public CreateBlogInteractor(
            IEfRepository<BlogDbContext, BlogContext.Domain.Blog> blogRepo,
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
                throw new ViolateSecurityException("Invalid Access.");

            var validationResult = _createBlogValidator.Validate(message);
            if (validationResult.IsValid == false)
                return await Task.FromResult(new CreateBlogResponse(Guid.Empty, validationResult));

            var blog = BlogContext.Domain.Blog.CreateInstance(message.Title, _securityContext.GetCurrentEmail())
                .ChangeDescription(message.Description)
                .ChangeSetting(
                    new BlogSetting(
                        IdHelper.GenerateId(),
                        message.PostsPerPage,
                        message.DaysToComment,
                        message.ModerateComments));

            var blogCreated = await _blogRepo.AddAsync(blog);

            // raise events
            foreach (var @event in blog.GetEvents())
                await _mediator.Publish(@event);

            return await Task.FromResult(new CreateBlogResponse(blogCreated.Id, validationResult));
        }
    }
}