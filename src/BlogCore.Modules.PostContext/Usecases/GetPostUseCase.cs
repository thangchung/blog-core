using BlogCore.Shared.v1.Post;
using BlogCore.Shared.v1.Usecase;
using BlogCore.Shared;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Threading.Tasks;

namespace BlogCore.Modules.PostContext.Usecases
{
    public class GetPostUseCase : IUseCase<GetPostRequest, GetPostResponse>
    {
        public async Task<GetPostResponse> ExecuteAsync(GetPostRequest request)
        {
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
                Slug = $"This is post {request.PostId}".GenerateSlug(),
                CreatedAt = Timestamp.FromDateTime(DateTime.UtcNow.Date).ToString(),
            };

            return new GetPostResponse { Post = post };
        }
    }
}
