using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogCore.Post.Domain
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetFullPostByBlogIdAsync(Guid blogId, int page);
    }
}