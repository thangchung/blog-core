using BlogCore.Infrastructure.UseCase;
using System;

namespace BlogCore.BlogContext.UseCases.BasicCrud
{
    public class RetrieveBlogRequest : IRequest<RetrieveBlogResponse>
    {
        public RetrieveBlogRequest(Guid id)
        {
            Id = id;            
        }

        public Guid Id { get; }
    }

    public class RetrieveBlogResponse
    {
        public RetrieveBlogResponse(
            Guid id,
            string title,
            string description,
            int theme,
            string image)
        {
            Id = id;
            Title = title;
            Description = description;
            Theme = theme;
            Image = image;
        }

        public Guid Id { get; }
        public string Title { get; }
        public string Description { get; }
        public int Theme { get; }
        public string Image { get; }
    }
}