using BlogCore.Shared.v1.Blog;
using BlogCore.Shared.v1.Usecase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Modules.BlogContext.Usecases
{
    public class RetrieveBlogsUseCase : IUseCase<RetrieveBlogsRequest, RetrieveBlogsResponse>
    {
        public async Task<RetrieveBlogsResponse> ExecuteAsync(RetrieveBlogsRequest request)
        {
            // TODO: get from database
            // ...

            var blogs = new List<BlogDto>();
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

                blogs.Add(blog);
            }

            var pager = new RetrieveBlogsResponse();
            pager.Items.AddRange(blogs.ToList());
            pager.TotalItems = 1;
            pager.TotalPages = 1;

            return await Task.FromResult(pager);
        }
    }
}
