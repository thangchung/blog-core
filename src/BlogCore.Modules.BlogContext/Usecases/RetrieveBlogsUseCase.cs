using BlogCore.Shared.v1.Blog;
using BlogCore.Shared.v1.Guard;
using BlogCore.Shared.v1.Usecase;
using BlogCore.Shared.v1.ValidationModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogCore.Modules.BlogContext.Usecases
{
    public class RetrieveBlogsUseCase : IUseCase<RetrieveBlogsRequest, PaginatedBlogResponse>
    {
        private readonly IValidator<RetrieveBlogsRequest> _validator;
        public RetrieveBlogsUseCase(IValidator<RetrieveBlogsRequest> validator)
        {
            _validator = validator.NotNull();
        }

        public async Task<PaginatedBlogResponse> ExecuteAsync(RetrieveBlogsRequest request)
        {
            await _validator.HandleValidation(request);

            // TODO: get from database
            // ...

            var blogs = new List<BlogDto>();
            for (var index = 1; index < 100; index++)
            {
                blogs.Add(new BlogDto
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = $"My blog {index}",
                    Description = $"This is my blog {index}",
                    Image = $"/images/my-blog-{index}.png",
                    Theme = 1
                });
            }
            
            var pager = new PaginatedBlogResponse();
            pager.Items.AddRange(blogs);
            pager.TotalItems = 1;
            pager.TotalPages = 1;

            return await Task.FromResult(pager);
        }
    }
}
