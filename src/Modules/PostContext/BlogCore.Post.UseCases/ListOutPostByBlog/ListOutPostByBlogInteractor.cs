using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BlogCore.Post.Domain;
using MediatR;

namespace BlogCore.Post.UseCases.ListOutPostByBlog
{
    public class ListOutPostByBlogInteractor : IAsyncRequestHandler<ListOutPostByBlogRequest,
        ListOutPostByBlogResponse>
    {
        private readonly IPostRepository _postRepository;

        public ListOutPostByBlogInteractor(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<ListOutPostByBlogResponse> Handle(ListOutPostByBlogRequest message)
        {
            var posts = await _postRepository.GetFullPostByBlogIdAsync(message.BlogId, message.Page);
            var responses = posts.Select(post =>
                new InnerListOutPostByBlogResponse
                {
                    Id = post.Id,
                    Title = post.Title,
                    Excerpt = post.Excerpt,
                    Slug = post.Slug,
                    CreatedAt = post.CreatedAt,
                    Author = post.Author,
                    Tags = post.Tags.ToList()
                });

            return new ListOutPostByBlogResponse
            {
                Inners = responses.ToList(),
                BlogId = message.BlogId,
                Page = message.Page,
                Total = _postRepository.GetTotalPost(message.BlogId)
            };
        }
    }
}