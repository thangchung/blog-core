using BlogCore.Shared.v1.Guard;
using BlogCore.Shared.v1.Post;
using BlogCore.Shared.v1.Usecase;
using BlogCore.Shared.v1.ValidationModel;
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

            var pager = new GetPostsByBlogResponse
            {
                TotalItems = 1,
                TotalPages = 1,
            };

            for (int i = 1; i < 50; i++)
            {
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
                    AuthorFamilyName = "Thang",
                    AuthorGivenName = "Chung",
                    Slug = $"This is post {i}".GenerateSlug(),
                    CreatedAt = Timestamp.FromDateTime(DateTime.UtcNow.Date).ToString(),
                };

                pager.Posts.Add(post);

                var linkTag = new PostTagsDto();
                linkTag.Tags.Add(new TagDto { Id = Guid.NewGuid().ToString(), Name = "tag 1" });

                pager.TagFragment.Add(post.Id, linkTag);
            }
            
            return await Task.FromResult(pager);
        }
    }
}
