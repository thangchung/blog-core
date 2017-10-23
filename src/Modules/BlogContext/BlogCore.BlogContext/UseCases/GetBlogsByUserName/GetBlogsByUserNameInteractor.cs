using BlogCore.AccessControlContext.Core.Domain;
using BlogCore.BlogContext.Infrastructure;
using BlogCore.Core;
using BlogCore.Infrastructure.EfCore;
using BlogCore.Infrastructure.UseCase;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BlogCore.BlogContext.UseCases.GetBlogsByUserName
{
    public class GetBlogsByUserNameInteractor
        : IUseCaseRequestHandlerAsync<GetBlogsByUserNameRequest, PaginatedItem<GetBlogsByUserNameResponse>>
    {
        private readonly IEfRepository<BlogDbContext, Core.Domain.Blog> _blogRepo;
        private readonly IUserRepository _userRepostory;
        public IOptions<PagingOption> _pagingOption;

        public GetBlogsByUserNameInteractor(
            IEfRepository<BlogDbContext, Core.Domain.Blog> blogRepo,
            IUserRepository userRepostory,
            IOptions<PagingOption> pagingOption
            )
        {
            _blogRepo = blogRepo;
            _userRepostory = userRepostory;
            _pagingOption = pagingOption;
        }

        public async Task<PaginatedItem<GetBlogsByUserNameResponse>> Process(GetBlogsByUserNameRequest request)
        {
            var user = await _userRepostory.GetByUserNameAsync(request.UserName);
            var criterion = new Criterion(request.Page, _pagingOption.Value.PageSize, _pagingOption.Value);
            Expression<Func<Core.Domain.Blog, bool>> filterFunc = x => x.OwnerEmail == user.Email;

            return await _blogRepo.ListStream(filterFunc, criterion)
                .Select(x =>
                {
                    return new PaginatedItem<GetBlogsByUserNameResponse>(
                        x.TotalItems,
                        (int)x.TotalPages,
                        x.Items.Select(y =>
                        {
                            return new GetBlogsByUserNameResponse
                            {
                                Id = y.Id,
                                Title = y.Title,
                                Description = y.Description,
                                Image = y.ImageFilePath,
                                Theme = (int)y.Theme
                            };
                        }).ToList()
                    );
                });
        }
    }
}
