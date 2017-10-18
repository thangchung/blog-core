using BlogCore.AccessControlContext.Core.Domain;
using BlogCore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BlogCore.PostContext.UseCases.ListOutPostByBlog
{
    public class ListOutPostByBlogPresenter
    {
        private readonly IUserRepository _userRepository;

        public ListOutPostByBlogPresenter(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PaginatedItem<ListOutPostByBlogResponse>> Transform(IObservable<PaginatedItem<ListOutPostByBlogResponse>> stream)
        {
            var result = await stream.Select(x => x);

            var authors = result.Items
                .Select(x => x.Author.Id)
                .Distinct()
                .Select(y => _userRepository.GetByIdAsync(y).Result)
                .ToList();

            var items = result.Items.Select(x =>
            {
                var author = authors.FirstOrDefault(au => au.Id == x.Author.Id.ToString());
                return x.SetAuthor(author?.Id, author?.FamilyName, author?.GivenName);
            });

            return new PaginatedItem<ListOutPostByBlogResponse>(
                result.TotalItems,
                (int)result.TotalPages,
                items.ToList());
        }
    }
}
