using BlogCore.Shared.v1;
using BlogCore.Shared.v1.Blog;
using BlogCore.Shared.v1.Presenter;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Modules.BlogContext.Usecases.Shared
{
    public class PaginatedBlogPresenter : IApiPresenter<PaginatedBlogResponse>
    {
        public dynamic OkResult { get; private set; }

        public void Handle(PaginatedBlogResponse response)
        {
            OkResult = new OkObjectResult(new ProtoResultModel<PaginatedBlogResponse>(response));
        }
    }
}
