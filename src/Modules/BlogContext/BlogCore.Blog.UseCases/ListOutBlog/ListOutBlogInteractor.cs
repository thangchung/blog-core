using System.Threading.Tasks;
using BlogCore.Blog.Infrastructure;
using MediatR;
using BlogCore.Infrastructure.EfCore;
using System.Linq.Expressions;
using System;
using BlogCore.Core;
using Microsoft.Extensions.Options;

namespace BlogCore.Blog.UseCases.ListOutBlog
{
    public class ListOutBlogInteractor : IAsyncRequestHandler<ListOutBlogRequest, PaginatedItem<ListOutBlogResponse>>
    {
        private readonly IEfRepository<BlogDbContext, Domain.Blog> _blogRepo;
        public IOptions<PagingOption> _pagingOption;

        public ListOutBlogInteractor(
            IEfRepository<BlogDbContext, Domain.Blog> blogRepo,
            IOptions<PagingOption> pagingOption)
        {
            _blogRepo = blogRepo;
            _pagingOption = pagingOption;
        }

        public async Task<PaginatedItem<ListOutBlogResponse>> Handle(ListOutBlogRequest request)
        {
            var criterion = new Criterion(1, _pagingOption.Value.PageSize, _pagingOption.Value);

            Expression<Func<Domain.Blog, ListOutBlogResponse>> selector =
                b => new ListOutBlogResponse(b.Id, b.Title, b.Description, b.ImageFilePath, (int)b.Theme);

            return await _blogRepo.QueryAsync(criterion, selector);
        }
    }
}