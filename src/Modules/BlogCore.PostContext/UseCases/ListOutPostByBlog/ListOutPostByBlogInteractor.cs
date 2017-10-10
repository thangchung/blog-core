using BlogCore.AccessControlContext.Domain;
using BlogCore.Core;
using BlogCore.Infrastructure.EfCore;
using BlogCore.PostContext.Infrastructure;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BlogCore.PostContext.UseCases.ListOutPostByBlog
{
    public class ListOutPostByBlogInteractor : IAsyncRequestHandler<ListOutPostByBlogRequest,
        PaginatedItem<ListOutPostByBlogResponse>>
    {
        private readonly IEfRepository<PostDbContext, Domain.Post> _postRepository;
        private readonly IUserRepository _userRepository;
        public IOptions<PagingOption> _pagingOption;

        public ListOutPostByBlogInteractor(
            IEfRepository<PostDbContext, Domain.Post> postRepository,
            IUserRepository userRepository,
            IOptions<PagingOption> pagingOption)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _pagingOption = pagingOption;
        }

        public async Task<PaginatedItem<ListOutPostByBlogResponse>> Handle(ListOutPostByBlogRequest request)
        {
            var criterion = new Criterion(request.Page, _pagingOption.Value.PageSize, _pagingOption.Value);
            var includes = new Expression<Func<Domain.Post, object>>[] { p => p.Comments, p => p.Author, p => p.Blog, p => p.Tags };
            Expression<Func<Domain.Post, bool>> filterFunc = x => x.Blog.Id == request.BlogId;

            return await _postRepository.ListStream(filterFunc, criterion, includes)
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