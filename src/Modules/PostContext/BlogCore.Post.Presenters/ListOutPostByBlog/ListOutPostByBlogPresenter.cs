using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.AccessControl.Domain;
using BlogCore.Core;
using BlogCore.Post.UseCases.ListOutPostByBlog;

namespace BlogCore.Post.Presenters.ListOutPostByBlog
{
    public class ListOutPostByBlogPresenter : IEnumerableOutputBoundary<
        IEnumerable<ListOutPostByBlogResponse>,
        IEnumerable<SimplePostViewModel>>
    {
        private readonly IUserRepository _userRepository;

        public ListOutPostByBlogPresenter(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<SimplePostViewModel>> TransformAsync(IEnumerable<ListOutPostByBlogResponse> posts)
        {
            var response = new List<SimplePostViewModel>();
            foreach (var post in posts)
            {
                var user = await _userRepository.GetByIdAsync(post.Author.Id);

                response.Add(new SimplePostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Excerpt = post.Excerpt,
                    Slug = post.Slug,
                    CreatedAt = post.CreatedAt,
                    Author = new AuthorViewModel { Id = IdHelper.GenerateId(user.Id), FamilyName = user.FamilyName, GivenName = user.GivenName },
                    Tags = post.Tags.Select(x=> new TagViewModel
                    {
                        Id = x.Id,
                        Name = x.Name
                    }).ToList()
                });
            }
            return response;
        }
    }
}