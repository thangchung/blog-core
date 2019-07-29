using BlogCore.Shared.v1.Blog;
using BlogCore.Shared.v1.Usecase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogCore.Modules.BlogContext.Usecases
{
    public class GetBlogByUserNameUseCase : IUseCase<GetMyBlogsRequest, GetMyBlogsResponse>
    {
        public async Task<GetMyBlogsResponse> ExecuteAsync(GetMyBlogsRequest request)
        {
            // TODO: get from database
            // ...

            var pager = new GetMyBlogsResponse
            {
                TotalItems = 1,
                TotalPages = 1,
            };

            var blog = new BlogDto
            {
                Id = Guid.NewGuid().ToString(),
                Title = "My blog",
                Description = "This is my blog",
                Image = "/images/my-blog.png",
                Theme = 1
            };

            pager.Items.AddRange(new List<BlogDto> { blog });
            return await Task.FromResult(pager);
        }
    }
}
