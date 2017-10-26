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
            await _createItemValidator.ValidateRequestAsync(request);

            return await _blogRepository.CreateItemProcessAsync(
                request,
                (requested =>
                {
                    return Core.Domain.Blog.CreateInstance(requested.Title, "dummyAuthor")
                        .ChangeDescription(requested.Description)
                        .ChangeSetting(
                            new BlogSetting(
                                IdHelper.GenerateId(),
                                requested.PostsPerPage,
                                requested.DaysToComment,
                                requested.ModerateComments));
                }),
                (blogCreated) =>
                {
                    return new CreateBlogResponse(blogCreated);
                },
                async (blogCreated) =>
                {
                    await _mediator.RaiseEventAsync(blogCreated);
                });
        }

        public async Task<PaginatedItem<RetrieveBlogsResponse>> ProcessAsync(RetrieveBlogsRequest request)
        {
            return await _blogRepository.RetrieveItemsProcessAsync(
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
            return await _blogRepository.RetrieveItemProcessAsync(
                request.Id,
                blog =>
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
            await _updateItemValidator.ValidateRequestAsync(request);

            return await _blogRepository.UpdateItemProcessAsync(
                request.Id,
                (updateBlog) =>
                {
                    return updateBlog.ChangeTitle(request.Title)
                        .ChangeDescription(request.Description)
                        .ChangeTheme((Theme)request.Theme);
                },
                (blogUpdated) =>
                {
                    return new UpdateBlogResponse
                    {
                        Id = blogUpdated.Id,
                        Title = blogUpdated.Title,
                        Description = blogUpdated.Description,
                        Theme = (int)blogUpdated.Theme
                    };
                },
                async (blogUpdated) =>
                {
                    await _mediator.RaiseEventAsync(blogUpdated);
                },
                x => x.BlogSetting);
        }

        public async Task<DeleteBlogResponse> ProcessAsync(DeleteBlogRequest request)
        {
            await _deleteItemValidator.ValidateRequestAsync(request);

            return await _blogRepository.DeleteItemProcessAsync(
                request.Id,
                (blog) =>
                {
                    return new DeleteBlogResponse(blog.Id);
                },
                async (blogDeleted) =>
                {
                    await _mediator.RaiseEventAsync(blogDeleted);
                });
        }
    }
}
