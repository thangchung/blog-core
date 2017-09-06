using MediatR;

namespace BlogCore.BlogContext.UseCases.Crud
{
    public class CreateBlogRequest : IRequest<CreateBlogResponse>
    {
        public CreateBlogRequest(
            string title, 
            string description,
            string theme, 
            int? postsPerPage, 
            int? daysToComment, 
            bool? moderateComments)
        {
            Title = title;
            Description = description;
            Theme = theme ?? "default";
            PostsPerPage = postsPerPage ?? 10;
            DaysToComment = daysToComment ?? 5;
            ModerateComments = moderateComments ?? true;
        }

        public string Title { get; }
        public string Description { get; }
        public string Theme { get; }
        public int PostsPerPage { get; }
        public int DaysToComment { get; }
        public bool ModerateComments { get; }
    }
}