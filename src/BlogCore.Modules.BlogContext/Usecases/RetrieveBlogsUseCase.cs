using BlogCore.Shared;
using BlogCore.Shared.v1.Blog;
using BlogCore.Shared.v1.Common;
using BlogCore.Shared.v1.Guard;
using BlogCore.Shared.v1.Usecase;
using BlogCore.Shared.v1.ValidationModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Modules.BlogContext.Usecases
{
    public class RetrieveBlogsUseCase : IUseCase<RetrieveBlogsRequest, PaginatedItemResponse>
    {
        private readonly IValidator<RetrieveBlogsRequest> _validator;
        public RetrieveBlogsUseCase(IValidator<RetrieveBlogsRequest> validator)
        {
            _validator = validator.NotNull();
        }

        public async Task<PaginatedItemResponse> ExecuteAsync(RetrieveBlogsRequest request)
        {
            await _validator.HandleValidation(request);

            // TODO: get from database
            // ...

            var items = new List<ItemContainer>();
            for (var index = 1; index < 100; index++)
            {
                var blog = new BlogDto
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = $"My blog {index}",
                    Description = $"This is my blog {index}",
                    Image = $"/images/my-blog-{index}.png",
                    Theme = 1
                };

                items.Add(blog.SerializeData());
            }

            var pager = new PaginatedItemResponse();
            pager.Items.AddRange(items.ToList());
            pager.TotalItems = 1;
            pager.TotalPages = 1;

            return await Task.FromResult(pager);
        }
    }
}
