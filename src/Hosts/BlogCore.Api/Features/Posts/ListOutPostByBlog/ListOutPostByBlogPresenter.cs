using BlogCore.AccessControlContext.Domain;
using BlogCore.Core;
using BlogCore.PostContext.UseCases.ListOutPostByBlog;
using System.Collections.Generic;
using System.Linq;

namespace BlogCore.Api.Features.Posts.ListOutPostByBlog
{
    public class ListOutPostByBlogPresenter
    {
        private readonly IUserRepository _userRepository;

        public ListOutPostByBlogPresenter(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public PaginatedItem<ListOutPostByBlogResponse> Transform(PaginatedItem<ListOutPostByBlogResponse> input)
        {
            var authors = input.Items
                .Select(x => x.Author.Id)
                .Distinct()
                .Select(x => _userRepository.GetByIdAsync(x).Result)
                .ToList();

            var posts = input.Items
                .Select(x =>
                {
                    var author = authors.FirstOrDefault(y => y.Id == x.Author.Id.ToString());
                    return x.SetAuthor(new ListOutPostByBlogUserResponse(author.Id, author.FamilyName, author.GivenName));
                })
                .ToList();

            return new PaginatedItem<ListOutPostByBlogResponse>(
                input.TotalItems,
                (int)input.TotalPages,
                posts);
        }
    }
}
