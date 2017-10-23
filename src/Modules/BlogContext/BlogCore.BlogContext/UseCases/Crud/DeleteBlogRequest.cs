using BlogCore.Infrastructure.UseCase;
using System;

namespace BlogCore.BlogContext.UseCases.Crud
{
    public class DeleteBlogRequest : IRequest<DeleteBlogResponse>
    {
        public Guid Id { get; set; }
    }
}
