using FluentValidation.Results;

namespace BlogCore.Core.BlogFeature
{
    public class ListOfBlogResponseMsg : IMesssage
    {
        public ListOfBlogResponseMsg(
            ValidationResult validationResult, 
            string title, 
            string description, 
            string image)
        {
            ValidationResult = validationResult;
            Title = title;
            Description = description;
            Image = image;
        }

        public ValidationResult ValidationResult { get; } 
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
    }
}