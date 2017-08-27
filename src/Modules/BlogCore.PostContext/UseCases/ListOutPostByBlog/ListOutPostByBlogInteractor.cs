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
        // private readonly IUserRepository _userRepository;
        public IOptions<PagingOption> _pagingOption;

        public ListOutPostByBlogInteractor(
            IEfRepository<PostDbContext, Domain.Post> postRepository,
            // IUserRepository userRepository,
            IOptions<PagingOption> pagingOption)
        {
            _postRepository = postRepository;
            // _userRepository = userRepository;
            _pagingOption = pagingOption;
        }

        public async Task<PaginatedItem<ListOutPostByBlogResponse>> Handle(ListOutPostByBlogRequest request)
        {
            var criterion = new Criterion(request.Page, _pagingOption.Value.PageSize, _pagingOption.Value, "CreatedAt");

            Expression<Func<Domain.Post, ListOutPostByBlogResponse>> selector = p => new ListOutPostByBlogResponse
            (
                p.Id,
                p.Title,
                p.Excerpt,
                p.Slug,
                p.CreatedAt,

                //TODO: will refactor to use the Send command (sync)
                new ListOutPostByBlogUserResponse(Guid.NewGuid().ToString(), "", ""),
                // new ListOutPostByBlogUserResponse(_userRepository.GetByIdAsync(p.Author.Id).Result),

                p.Tags.Select(t => new ListOutPostByBlogTagResponse(t.Id, t.Name)).ToList()
            );

            return await _postRepository.FindAllAsync(
                criterion,
                selector,
                x => x.Blog.Id == request.BlogId,
                p => p.Comments,
                p => p.Author,
                p => p.Blog,
                p => p.Tags);
        }
    }
}