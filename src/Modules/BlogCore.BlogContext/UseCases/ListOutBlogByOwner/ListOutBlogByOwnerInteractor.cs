using BlogCore.BlogContext.Infrastructure;
using BlogCore.BlogContext.UseCases.ListOutBlogByOwner;
using BlogCore.Core;
using BlogCore.Infrastructure.EfCore;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogCore.Blog.UseCases.ListOutBlogByOwner
{
    public class ListOutBlogByOwnerInteractor : IAsyncRequestHandler<ListOutBlogByOwnerRequest, PaginatedItem<ListOutBlogByOwnerResponse>>
    {
        private readonly IEfRepository<BlogDbContext, BlogContext.Domain.Blog> _blogRepo;
        private readonly ISecurityContext _securityContext;
        public IOptions<PagingOption> _pagingOption;

        public ListOutBlogByOwnerInteractor(
            IEfRepository<BlogDbContext, BlogContext.Domain.Blog> blogRepo, 
            ISecurityContext securityContext,
            IOptions<PagingOption> pagingOption)
        {
            _blogRepo = blogRepo;
            _securityContext = securityContext;
            _pagingOption = pagingOption;
        }

        public async Task<PaginatedItem<ListOutBlogByOwnerResponse>> Handle(ListOutBlogByOwnerRequest request)
        {
            var criterion = new Criterion(1, int.MaxValue, _pagingOption.Value);
            Expression<Func<BlogContext.Domain.Blog, ListOutBlogByOwnerResponse>> selector =
                b => new ListOutBlogByOwnerResponse(b.Id, b.Title, b.Description, b.ImageFilePath, (int)b.Theme);
            Expression<Func<BlogContext.Domain.Blog, bool>> ownerEmailFilter = x => x.OwnerEmail == _securityContext.GetCurrentEmail();
            return await _blogRepo.FindAllAsync(criterion, selector, ownerEmailFilter);
        }
    }
}