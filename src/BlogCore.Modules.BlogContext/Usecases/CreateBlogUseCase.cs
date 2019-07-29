using BlogCore.Shared.v1.Blog;
using BlogCore.Shared.v1.Usecase;
using System;
using System.Threading.Tasks;

namespace BlogCore.Modules.BlogContext.Usecases
{
    public class CreateBlogUseCase : IUseCase<CreateBlogRequest, CreateBlogResponse>
    {
        public async Task<CreateBlogResponse> ExecuteAsync(CreateBlogRequest request)
        {
            //TODO: save to database
            //...

            return await Task.FromResult(new CreateBlogResponse {
                Blog = new BlogDto {
                    Id = Guid.NewGuid().ToString(),
                    Title = "created",
                    Description = "created desc"
                }
            });
        }
    }
}
