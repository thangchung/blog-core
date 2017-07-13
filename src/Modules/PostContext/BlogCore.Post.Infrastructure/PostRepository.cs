using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogCore.Post.Domain;

namespace BlogCore.Post.Infrastructure
{
    public class PostRepository : IPostRepository
    {
        private readonly PostDbContext _dbContext;

        public PostRepository(PostDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Domain.Post>> GetFullPostByBlogIdAsync(Guid blogId, int page)
        {
            return await _dbContext
                .Set<Domain.Post>()
                .GetPostByPage(blogId, page);
        }
    }
}