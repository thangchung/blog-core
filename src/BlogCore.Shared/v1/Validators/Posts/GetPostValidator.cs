using BlogCore.Shared.v1.Post;
using FluentValidation;

namespace BlogCore.Shared.v1.Validators.Posts
{
    public class GetPostValidator : AbstractValidator<GetPostRequest>
    {
        public GetPostValidator()
        {

        }
    }
}
