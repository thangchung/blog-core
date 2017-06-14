namespace BlogCore.Api.Blogs
{
    public class BlogPresenterFactory
    {
        public CreateBlogPresenter CreateBlogPresenterInstance()
        {
            return new CreateBlogPresenter();
        }

        public GetBlogPresenter GetBlogPresenterInstance()
        {
            return new GetBlogPresenter();
        }

        public ListOfBlogPresenter ListOfBlogPresenterInstance()
        {
            return new ListOfBlogPresenter();
        }
    }
}