using BlogCore.Core;
using BlogCore.Infrastructure.EfCore;
using BlogCore.Infrastructure.UseCase;
using BlogCore.PostContext.Core.Domain;
using BlogCore.PostContext.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;

namespace BlogCore.PostContext.UseCases.ListOutPostByBlog
{
    public class ListOutPostByBlogInteractor 
        : IUseCaseRequestHandler<ListOutPostByBlogRequest, PaginatedItem<ListOutPostByBlogResponse>>
    {
        private readonly IEfRepository<PostDbContext, Post> _postRepository;
        public IOptions<PagingOption> _pagingOption;

        public ListOutPostByBlogInteractor(
            IEfRepository<PostDbContext, Post> postRepository,
            IOptions<PagingOption> pagingOption)
        {
            _postRepository = postRepository;
            _pagingOption = pagingOption;
        }

        public IObservable<PaginatedItem<ListOutPostByBlogResponse>> Process(ListOutPostByBlogRequest request)
        {
            var criterion = new Criterion(request.Page, _pagingOption.Value.PageSize, _pagingOption.Value);
            var includes = new Expression<Func<Post, object>>[] { p => p.Comments, p => p.Author, p => p.Blog, p => p.Tags };
            Expression<Func<Post, bool>> filterFunc = x => x.Blog.Id == request.BlogId;

            return _postRepository.ListStream(filterFunc, criterion, includes)
                .Select(y =>
                {
                    return new PaginatedItem<ListOutPostByBlogResponse>(
                            y.TotalItems,
                            (int)y.TotalPages,
                            y.Items.Select(x =>
                            {
                                return new ListOutPostByBlogResponse(
                                    x.Id,
                                    x.Title,
                                    x.Excerpt,
                                    x.Slug,
                                    x.CreatedAt,
                                    new ListOutPostByBlogUserResponse(
                                            x.Author.Id.ToString(),
                                            string.Empty,
                                            string.Empty
                                        ),
                                    x.Tags.Select(
                                        tag => new ListOutPostByBlogTagResponse(
                                            tag.Id,
                                            tag.Name))
                                        .ToList()
                                );
                            }).ToList()
                        );
                });
        }
    }
}