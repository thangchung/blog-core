using BlogCore.AccessControlContext.Domain;
using BlogCore.Core;
using BlogCore.Infrastructure.EfCore;
using BlogCore.PostContext.Infrastructure;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
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
            return await _postRepository.ListStream(p => p.Comments, p => p.Author, p => p.Blog, p => p.Tags)
                .Select(list => {
                    var authors = list.Select(p => p.Author.Id)
                        .Distinct()
                        .Select(id => _userRepository.GetByIdStream(id).Wait())
                        .ToList();

                    return list.Where(x => x.Blog.Id == request.BlogId)
                            .Skip(request.Page * _pagingOption.Value.PageSize)
                            .Take(_pagingOption.Value.PageSize)
                            .Select(x =>
                            {
                                var author = authors.FirstOrDefault(y => y.Id == x.Author.Id.ToString());

                                return new ListOutPostByBlogResponse(
                                    x.Id,
                                    x.Title,
                                    x.Excerpt,
                                    x.Slug,
                                    x.CreatedAt,
                                    new ListOutPostByBlogUserResponse(
                                            author.Id,
                                            author.FamilyName,
                                            author.GivenName
                                        ),
                                    x.Tags.Select(
                                            tag => new ListOutPostByBlogTagResponse(
                                                tag.Id,
                                                tag.Name))
                                        .ToList()
                                );
                            });
                })
                .Select(results =>
                {
                    return new PaginatedItem<ListOutPostByBlogResponse>(
                        results.Count(),
                        (int)Math.Ceiling((double)results.Count() / _pagingOption.Value.PageSize),
                        results.ToList());
                });
        }
    }
}