using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using MediatR;
using BlogCore.Core;
using BlogCore.Post.Infrastructure;
using BlogCore.Infrastructure.EfCore;
using System.Linq.Expressions;
using System;
using BlogCore.AccessControl.Domain;

namespace BlogCore.Post.UseCases.ListOutPostByBlog
{
    public class ListOutPostByBlogInteractor : IAsyncRequestHandler<ListOutPostByBlogRequest,
        PaginatedItem<ListOutPostByBlogResponse>>
    {
        private readonly IEfRepository<PostDbContext, Domain.Post> _postRepository;
        private readonly IUserRepository _userRepository;

        public ListOutPostByBlogInteractor(
            IEfRepository<PostDbContext, Domain.Post> postRepository,
            IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<PaginatedItem<ListOutPostByBlogResponse>> Handle(ListOutPostByBlogRequest request)
        {
            var criterion = new Criterion(request.Page, 10, "CreatedAt");
            Expression<Func<Domain.Post, ListOutPostByBlogResponse>> selector = p => new ListOutPostByBlogResponse
            {
                Id = p.Id,
                Title = p.Title,
                Slug = p.Slug,
                Excerpt = p.Excerpt,
                CreatedAt = p.CreatedAt,
                Author = new ListOutPostByBlogUserResponse(_userRepository.GetByIdAsync(p.Author.Id).Result),
                Tags = p.Tags.Select(t => new ListOutPostByBlogTagResponse
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToList()
            };

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