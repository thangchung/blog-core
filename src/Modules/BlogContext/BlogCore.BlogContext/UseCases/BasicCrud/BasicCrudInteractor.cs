using BlogCore.BlogContext.Core.Domain;
using BlogCore.BlogContext.Infrastructure;
using BlogCore.Core;
using BlogCore.Core.Helpers;
using BlogCore.Infrastructure.EfCore;
using BlogCore.Infrastructure.UseCase;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using static BlogCore.BlogContext.UseCases.BasicCrud.CrudHelpers;

namespace BlogCore.BlogContext.UseCases.BasicCrud
{
    public class BasicCrudInteractor : 
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
        protected readonly IValidator<UpdateBlogRequest> _updateItemValidator;
        protected readonly IValidator<DeleteBlogRequest> _deleteItemValidator;

        public BasicCrudInteractor(
            IMediator mediator,
            IOptions<PagingOption> pagingOption,
            IEfRepository<BlogDbContext, Core.Domain.Blog> blogRepository,
            IValidator<CreateBlogRequest> createItemValidator,
            IValidator<UpdateBlogRequest> updateItemValidator,
            IValidator<DeleteBlogRequest> deleteItemValidator)
        {
            _blogRepository = blogRepository;
            _mediator = mediator;
            _pagingOption = pagingOption;
            _createItemValidator = createItemValidator;
            _updateItemValidator = updateItemValidator;
            _deleteItemValidator = deleteItemValidator;
        }

        public async Task<CreateBlogResponse> ProcessAsync(CreateBlogRequest request)
        {
            return await CreateItemProcessAsync<Core.Domain.Blog, CreateBlogRequest, CreateBlogResponse>(
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

        public async Task<PaginatedItem<RetrieveBlogsResponse>> ProcessAsync(RetrieveBlogsRequest request)
        {
            return await RetrieveItemsProcessAsync(
                    _blogRepository,
                    _pagingOption,
                    request,
                    b => new RetrieveBlogsResponse(
                        b.Id,
                        b.Title,
                        b.Description,
                        b.ImageFilePath,
                        (int)b.Theme)
                );
        }

        public async Task<RetrieveBlogResponse> ProcessAsync(RetrieveBlogRequest request)
        {
            return await RetrieveItemProcessAsync(_blogRepository, request.Id, blog =>
                new RetrieveBlogResponse(
                blog.Id,
                blog.Title,
                blog.Description,
                (int)blog.Theme,
                blog.ImageFilePath)
            );
        }

        public async Task<UpdateBlogResponse> ProcessAsync(UpdateBlogRequest request)
        {
            return await UpdateItemProcessAsync(_blogRepository, request.Id,
                (blog) =>
                {
                    return blog.ChangeTitle(request.Title)
                        .ChangeDescription(request.Description)
                        .ChangeTheme((Theme)request.Theme);
                },
                (blog) =>
                {
                    return new UpdateBlogResponse
                    {
                        Id = blog.Id,
                        Title = blog.Title,
                        Description = blog.Description,
                        Theme = (int)blog.Theme
                    };
                });
        }

        public async Task<DeleteBlogResponse> ProcessAsync(DeleteBlogRequest request)
        {
            return await DeleteItemProcessAsync<BlogDbContext, Core.Domain.Blog, DeleteBlogResponse>(
                _blogRepository, request.Id);
        }
    }
}
