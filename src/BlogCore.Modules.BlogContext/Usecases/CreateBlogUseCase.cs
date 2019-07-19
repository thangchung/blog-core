using BlogCore.Shared.v1.Blog;
using BlogCore.Shared.v1.Guard;
using BlogCore.Shared.v1.Usecase;
using BlogCore.Shared.v1.ValidationModel;
using FluentValidation;
using System;
using System.Threading.Tasks;

namespace BlogCore.Modules.BlogContext.Usecases
{
    public class CreateBlogUseCase : IUseCase<CreateBlogRequest, CreateBlogResponse>
    {
        private readonly IValidator<CreateBlogRequest> _validator;

        public CreateBlogUseCase(IValidator<CreateBlogRequest> validator)
        {
            _validator = validator.NotNull();
        }

        public async Task<CreateBlogResponse> ExecuteAsync(CreateBlogRequest request)
        {
            await _validator.HandleValidation(request);

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
