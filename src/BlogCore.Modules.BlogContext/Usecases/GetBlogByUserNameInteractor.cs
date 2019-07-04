using BlogCore.Shared.v1.Blog;
using BlogCore.Shared.v1.ValidationModel;
using BlogCore.Shared.v1.Validators;
using NetCoreKit.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogCore.Modules.BlogContext.Usecases
{
    public class GetBlogByUsernameInteractor
    {
        private readonly GetMyBlogsRequestValidator _validator;

        public GetBlogByUsernameInteractor(GetMyBlogsRequestValidator validator)
        {
            _validator = validator;
        }

        public async Task<PaginatedItem<BlogDto>> Handle(GetMyBlogsRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new Shared.v1.ValidationModel.ValidationException(validationResult.ToValidationResultModel());
            }

            // TODO: get from database

            return await Task.FromResult(
                new PaginatedItem<BlogDto>(
                    1,
                    1,
                    new List<BlogDto> {
                        new BlogDto
                        {
                            Id = Guid.NewGuid().ToString(),
                            Title = "My blog",
                            Description = "This is my blog",
                            Image = "/images/my-blog.png",
                            Theme = 1
                        }
                    }));
        }
    }
}
