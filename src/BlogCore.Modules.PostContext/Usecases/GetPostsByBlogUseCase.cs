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
    public class GetPostsByBlogUseCase : IUseCase<GetPostsByBlogRequest, GetPostsByBlogResponse>
    {
        private readonly IValidator<GetPostsByBlogRequest> _validator;

        public GetPostsByBlogUseCase(IValidator<GetPostsByBlogRequest> validator)
        {
            _validator = validator.NotNull();
        }

        public async Task<GetPostsByBlogResponse> ExecuteAsync(GetPostsByBlogRequest request)
        {
            await _validator.HandleValidation(request);

            //TODO: save to database
            //...

            var pageSize = 10;
            var pager = new GetPostsByBlogResponse
            {
                TotalItems = 50,
                TotalPages = 5,
            };

            for (int i = (request.Page - 1) * pageSize + 1; i <= request.Page * pageSize; i++)
            {
                var tag1 = new TagDto { Id = Guid.NewGuid().ToString(), Name = "tag 1" };
                var tag2 = new TagDto { Id = Guid.NewGuid().ToString(), Name = "tag 2" };

                var post = new PostDto
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = $"This is post {i}",
                    Excerpt = @"Lorem ipsum represents a long-held tradition for designers, typographers and the like.Some people hate it and argue for its demise, but others ignore the hate as they create awesome tools to help create filler text for everyone from bacon lovers to Charlie Sheen fans.",
                    Body = @"
                    Lorem ipsum represents a long-held tradition for designers, typographers and the like.Some people hate it and argue for its demise, but others ignore the hate as they create awesome tools to help create filler text for everyone from bacon lovers to Charlie Sheen fans.
                    Lorem ipsum represents a long-held tradition for designers, typographers and the like.Some people hate it and argue for its demise, but others ignore the hate as they create awesome tools to help create filler text for everyone from bacon lovers to Charlie Sheen fans.
                    Lorem ipsum represents a long-held tradition for designers, typographers and the like.Some people hate it and argue for its demise, but others ignore the hate as they create awesome tools to help create filler text for everyone from bacon lovers to Charlie Sheen fans.
                    Lorem ipsum represents a long-held tradition for designers, typographers and the like.Some people hate it and argue for its demise, but others ignore the hate as they create awesome tools to help create filler text for everyone from bacon lovers to Charlie Sheen fans.
                    Lorem ipsum represents a long-held tradition for designers, typographers and the like.Some people hate it and argue for its demise, but others ignore the hate as they create awesome tools to help create filler text for everyone from bacon lovers to Charlie Sheen fans.
                    ",
                    AuthorId = Guid.NewGuid().ToString(),
                    Slug = $"This is post {i}".GenerateSlug(),
                    CreatedAt = Timestamp.FromDateTime(DateTime.UtcNow.Date).ToString(),
                };

                post.Tags.AddRange(new[] { tag1, tag2 });
                pager.Posts.Add(post);
            }
            
            return await Task.FromResult(pager);
        }
    }
}
