using BlogCore.Shared.v1.Blog;
using BlogCore.Shared.v1.Usecase;
using BlogCore.Shared.v1.Guard;
using FluentValidation;
using System.Threading.Tasks;
using BlogCore.Shared.v1.ValidationModel;

namespace BlogCore.Modules.BlogContext.Usecases
{
    public class GetBlogInfoUseCase : IUseCase<GetBlogInfoRequest, GetBlogInfoResponse>
    {
        private readonly IValidator<GetBlogInfoRequest> _validator;

        public GetBlogInfoUseCase(IValidator<GetBlogInfoRequest> validator)
        {
            _validator = validator.NotNull();
        }

        public async Task<GetBlogInfoResponse> ExecuteAsync(GetBlogInfoRequest request)
        {
            await _validator.HandleValidation(request);
            var blog = new BlogDto { Id = request.BlogId, Title = $"My Blog {request.BlogId}", Description = $"This is description for {request.BlogId}" };
            return await Task.FromResult(new GetBlogInfoResponse { Blog = blog });
        }
    }
}
