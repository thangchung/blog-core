using System.Threading.Tasks;
using BlogCore.Blog.Infrastructure;
using MediatR;
using BlogCore.Infrastructure.EfCore;
using System.Linq.Expressions;
using System;

namespace BlogCore.Blog.UseCases.ListOutBlog
{
    public class ListOutBlogInteractor : IAsyncRequestHandler<ListOutBlogRequest, PaginatedItem<ListOutBlogResponse>>
    {
        private readonly IEfRepository<BlogDbContext, Domain.Blog> _blogRepo;

        public ListOutBlogInteractor(IEfRepository<BlogDbContext, Domain.Blog> blogRepo)
        {
            _blogRepo = blogRepo;
        }

        public async Task<PaginatedItem<ListOutBlogResponse>> Handle(ListOutBlogRequest request)
        {
            Expression<Func<Domain.Blog, ListOutBlogResponse>> selector =
                b => new ListOutBlogResponse(b.Id, b.Title, b.Description, b.ImageFilePath, (int)b.Theme);
            return await _blogRepo.QueryAsync(request.Criterion, selector);
        }
    }
}