using MediatR;

namespace BlogCore.BlogContext.UseCases.Crud
{
    public class DeleteBlogRequest : IRequest<DeleteBlogResponse>
    {
    }
}
