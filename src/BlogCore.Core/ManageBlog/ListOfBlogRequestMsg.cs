using System.Collections.Generic;
using MediatR;

namespace BlogCore.Core.ManageBlog
{
    public class ListOfBlogRequestMsg : IMesssage, IRequest<IEnumerable<ListOfBlogResponseMsg>>
    {
        // TODO: re-visit for paging info later         
    }
}