using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BlogCore.Post.Domain;
using MediatR;

namespace BlogCore.Post.UseCases.ListOutPostByBlog
{
    public class ListOutPostByBlogInteractor : IAsyncRequestHandler<ListOutPostByBlogRequest,
        IEnumerable<ListOutPostByBlogResponse>>
    {
        private readonly IPostRepository _postRepository;

        public ListOutPostByBlogInteractor(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<ListOutPostByBlogResponse>> Handle(ListOutPostByBlogRequest message)
        {
            var response = new List<ListOutPostByBlogResponse>();
            var posts = await _postRepository.GetFullPostByBlogIdAsync(message.BlogId, message.Page);
            var postObservable = posts.ToObservable();
            await postObservable.ForEachAsync(post =>
            {
                response.Add(new ListOutPostByBlogResponse
                {
                    Id = post.Id,
                    Title = post.Title,
                    Excerpt = post.Excerpt,
                    Slug = post.Slug,
                    CreatedAt = post.CreatedAt,
                    Author = post.Author,
                    Tags = post.Tags.ToList()
                });
            });
            return response;
        }
    }
}