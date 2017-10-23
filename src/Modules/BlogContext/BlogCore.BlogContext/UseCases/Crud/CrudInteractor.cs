using BlogCore.BlogContext.Core.Domain;
using BlogCore.BlogContext.Infrastructure;
using BlogCore.Core;
using BlogCore.Core.Helpers;
using BlogCore.Infrastructure.EfCore;
using BlogCore.Infrastructure.UseCase;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace BlogCore.BlogContext.UseCases.Crud
{
    public class CrudInteractor : 
        IAsyncUseCaseRequestHandler<CreateBlogRequest, CreateBlogResponse>,
        IAsyncUseCaseRequestHandler<RetrieveBlogsRequest, PaginatedItem<RetrieveBlogsResponse>>,
        IAsyncUseCaseRequestHandler<RetrieveBlogRequest, RetrieveBlogResponse>,
        IAsyncUseCaseRequestHandler<UpdateBlogRequest, UpdateBlogResponse>,
        IAsyncUseCaseRequestHandler<DeleteBlogRequest, DeleteBlogResponse>
    {
        protected readonly IEfRepository<BlogDbContext, Core.Domain.Blog> _blogRepository;
        protected readonly IMediator _mediator;
        protected readonly IOptions<PagingOption> _pagingOption;
        protected readonly IValidator<CreateBlogRequest> _createItemValidator;

        public CrudInteractor(
            IValidator<CreateBlogRequest> createItemValidator,
            IEfRepository<BlogDbContext, Core.Domain.Blog> blogRepository,
            IMediator mediator,
            IOptions<PagingOption> pagingOption
            )
        {
            _blogRepository = blogRepository;
            _mediator = mediator;
            _pagingOption = pagingOption;
            _createItemValidator = createItemValidator;
        }

        public async Task<CreateBlogResponse> Process(CreateBlogRequest request)
        {
            return await CrudHelpers.CreateItemHandler<
                Core.Domain.Blog,
                CreateBlogRequest, CreateBlogResponse>(
                    _mediator,
                    request,
                    _createItemValidator,
                    (async r =>
                    {
                        var blog = Core.Domain.Blog.CreateInstance(r.Title, "dummyAuthor")
                            .ChangeDescription(r.Description)
                            .ChangeSetting(
                                new BlogSetting(
                                    IdHelper.GenerateId(),
                                    r.PostsPerPage,
                                    r.DaysToComment,
                                    r.ModerateComments));
                        var blogCreated = await _blogRepository.AddAsync(blog);
                        return await Task.FromResult(blogCreated);
                    }));
        }

        public async Task<PaginatedItem<RetrieveBlogsResponse>> Process(RetrieveBlogsRequest request)
        {
            return await CrudHelpers.RetrieveItemsHandler<
                BlogDbContext, Core.Domain.Blog,
                RetrieveBlogsRequest, RetrieveBlogsResponse>(
                    _blogRepository,
                    _pagingOption,
                    b => new RetrieveBlogsResponse(
                        b.Id,
                        b.Title,
                        b.Description,
                        b.ImageFilePath,
                        (int)b.Theme)
                );
        }

        public async Task<RetrieveBlogResponse> Process(RetrieveBlogRequest request)
        {
            var blog = await _blogRepository.GetByIdAsync(request.Id);
            return new RetrieveBlogResponse(
                blog.Id,
                blog.Title,
                blog.Description,
                (int)blog.Theme,
                blog.ImageFilePath);
        }

        public async Task<UpdateBlogResponse> Process(UpdateBlogRequest request)
        {
            var blog = await _blogRepository.GetByIdAsync(request.Id);
            var blogUpdated = blog.ChangeTitle(request.Title)
                .ChangeDescription(request.Description)
                .ChangeTheme((Theme)request.Theme);


            return await _blogRepository.UpdateAsync(blogUpdated)
                .ContinueWith((a) =>
                {
                    return new UpdateBlogResponse();
                });
        }

        public async Task<DeleteBlogResponse> Process(DeleteBlogRequest request)
        {
            var blog = await _blogRepository.GetByIdAsync(request.Id);
            await _blogRepository.DeleteAsync(blog);
            return new DeleteBlogResponse();
        }
    }


    public static class CrudHelpers
    {
        public static async Task<TCreateItemResponse> CreateItemHandler<TEntity, TCreateItemRequest, TCreateItemResponse>(
            IMediator mediator,
            TCreateItemRequest request,
            IValidator<TCreateItemRequest> createItemValidator,
            Func<TCreateItemRequest, Task<TEntity>> extendCreatingItemFunc)
            where TEntity : EntityBase
            where TCreateItemRequest : BlogCore.Infrastructure.UseCase.IRequest<TCreateItemResponse>
        {
            // validate request
            var validationResult = createItemValidator.Validate(request);
            if (validationResult.IsValid == false)
            {
                return await Task.FromResult((TCreateItemResponse)Activator.CreateInstance(typeof(TCreateItemResponse), Guid.Empty, validationResult));
            }

            var entity = await extendCreatingItemFunc(request);

            // raise events
            foreach (var @event in entity.GetEvents())
                await mediator.Publish(@event);

            // compose and return values
            return await Task.FromResult(
                (TCreateItemResponse)Activator.CreateInstance(
                    typeof(TCreateItemResponse).GetTypeInfo(),
                    entity.Id,
                    validationResult));
        }

        public static async Task<PaginatedItem<TRetrieveItemsResponse>> RetrieveItemsHandler<TDbContext, TEntity, TCreateItemRequest, TRetrieveItemsResponse>(
            IEfRepository<TDbContext, TEntity> repo,
            IOptions<PagingOption> pagingOption,
            Expression<Func<TEntity, TRetrieveItemsResponse>> expr)
                where TEntity : EntityBase
                where TDbContext : DbContext
        {
            var criterion = new Criterion(1, pagingOption.Value.PageSize, pagingOption.Value);
            return await repo.QueryAsync(criterion, expr);
        }
    }
}
