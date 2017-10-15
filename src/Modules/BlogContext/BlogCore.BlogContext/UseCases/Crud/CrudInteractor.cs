using BlogCore.BlogContext.Core.Domain;
using BlogCore.BlogContext.Infrastructure;
using BlogCore.Core;
using BlogCore.Core.Helpers;
using BlogCore.Infrastructure.EfCore;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogCore.BlogContext.UseCases.Crud
{
    public class CrudInteractor : 
        IAsyncRequestHandler<CreateBlogRequest, CreateBlogResponse>,
        IAsyncRequestHandler<RetrieveBlogsRequest, PaginatedItem<RetrieveBlogsResponse>>,
        IAsyncRequestHandler<RetrieveBlogRequest, RetrieveBlogResponse>,
        IAsyncRequestHandler<UpdateBlogRequest, UpdateBlogResponse>,
        IAsyncRequestHandler<DeleteBlogRequest, DeleteBlogResponse>
    {
        private readonly IEfRepository<BlogDbContext, Core.Domain.Blog> _blogRepo;
        private readonly IValidator<CreateBlogRequest> _createBlogValidator;
        private readonly IMediator _mediator;
        private readonly ISecurityContext _securityContext;
        public IOptions<PagingOption> _pagingOption;

        public CrudInteractor(
            IEfRepository<BlogDbContext, Core.Domain.Blog> blogRepo,
            IValidator<CreateBlogRequest> createBlogValidator,
            IOptions<PagingOption> pagingOption,
            ISecurityContext securityContext,
            IMediator mediator
            )
        {
            _blogRepo = blogRepo;
            _createBlogValidator = createBlogValidator;
            _pagingOption = pagingOption;
            _securityContext = securityContext;
            _mediator = mediator;
        }

        public async Task<CreateBlogResponse> Handle(CreateBlogRequest message)
        {
            if (_securityContext.HasClaims() == false)
                throw new ViolateSecurityException("Invalid Access.");

            var validationResult = _createBlogValidator.Validate(message);
            if (validationResult.IsValid == false)
                return await Task.FromResult(new CreateBlogResponse(Guid.Empty, validationResult));

            var blog = Core.Domain.Blog.CreateInstance(message.Title, _securityContext.GetCurrentEmail())
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

        public async Task<PaginatedItem<RetrieveBlogsResponse>> Handle(RetrieveBlogsRequest message)
        {
            var criterion = new Criterion(1, _pagingOption.Value.PageSize, _pagingOption.Value);

            Expression<Func<Core.Domain.Blog, RetrieveBlogsResponse>> selector =
                b => new RetrieveBlogsResponse(b.Id, b.Title, b.Description, b.ImageFilePath, (int)b.Theme);

            return await _blogRepo.QueryAsync(criterion, selector);
        }

        public async Task<RetrieveBlogResponse> Handle(RetrieveBlogRequest message)
        {
            var blog = await _blogRepo.GetByIdAsync(message.Id);
            return new RetrieveBlogResponse(
                blog.Id,
                blog.Title,
                blog.Description,
                (int)blog.Theme,
                blog.ImageFilePath);
        }

        public Task<UpdateBlogResponse> Handle(UpdateBlogRequest message)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteBlogResponse> Handle(DeleteBlogRequest message)
        {
            throw new NotImplementedException();
        }
    }
}
