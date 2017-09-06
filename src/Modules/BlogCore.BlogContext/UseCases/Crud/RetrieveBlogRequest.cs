using System;
using MediatR;

namespace BlogCore.BlogContext.UseCases.Crud
{
    public class RetrieveBlogRequest : IRequest<RetrieveBlogResponse>
    {
        public RetrieveBlogRequest(Guid id)
        {
            Id = id;            
        }

        public Guid Id { get; }
    }
}