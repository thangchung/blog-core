using BlogCore.Shared.v1.Guard;
using BlogCore.Shared.v1.Post;
using BlogCore.Shared.v1.Usecase;
using BlogCore.Shared.v1.ValidationModel;
using BlogCore.Shared;
using FluentValidation;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Threading.Tasks;

namespace BlogCore.Modules.PostContext.Usecases
{
    public class GetPostUseCase : IUseCase<GetPostRequest, GetPostResponse>
    {
        private readonly IValidator<GetPostRequest> _validator;

        public GetPostUseCase(IValidator<GetPostRequest> validator)
        {
            _validator = validator.NotNull();
        }

        public async Task<GetPostResponse> ExecuteAsync(GetPostRequest request)
        {
            await _validator.HandleValidation(request);

            var post = new PostDto
            {
                Id = request.PostId,
                Title = $"This is post {request.PostId}",
                Excerpt = @"Lorem ipsum represents a long-held tradition for designers, typographers and the like.Some people hate it and argue for its demise, but others ignore the hate as they create awesome tools to help create filler text for everyone from bacon lovers to Charlie Sheen fans.",
                Body = @"
                    Lorem ipsum represents a long-held tradition for designers, typographers and the like.Some people hate it and argue for its demise, but others ignore the hate as they create awesome tools to help create filler text for everyone from bacon lovers to Charlie Sheen fans.
                    Lorem ipsum represents a long-held tradition for designers, typographers and the like.Some people hate it and argue for its demise, but others ignore the hate as they create awesome tools to help create filler text for everyone from bacon lovers to Charlie Sheen fans.
                    Lorem ipsum represents a long-held tradition for designers, typographers and the like.Some people hate it and argue for its demise, but others ignore the hate as they create awesome tools to help create filler text for everyone from bacon lovers to Charlie Sheen fans.
                    Lorem ipsum represents a long-held tradition for designers, typographers and the like.Some people hate it and argue for its demise, but others ignore the hate as they create awesome tools to help create filler text for everyone from bacon lovers to Charlie Sheen fans.
                    Lorem ipsum represents a long-held tradition for designers, typographers and the like.Some people hate it and argue for its demise, but others ignore the hate as they create awesome tools to help create filler text for everyone from bacon lovers to Charlie Sheen fans.
                    ",
                AuthorId = Guid.NewGuid().ToString(),
                AuthorFamilyName = "Thang",
                AuthorGivenName = "Chung",
                Slug = $"This is post {request.PostId}".GenerateSlug(),
                CreatedAt = Timestamp.FromDateTime(DateTime.UtcNow.Date).ToString(),
            };

            return new GetPostResponse { Post = post };
        }
    }
}
