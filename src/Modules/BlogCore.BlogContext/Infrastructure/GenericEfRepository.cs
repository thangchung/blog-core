using BlogCore.Core;
using BlogCore.Infrastructure.EfCore;

namespace BlogCore.BlogContext.Infrastructure
{
    public class BlogEfRepository<TEntity> : EfRepository<BlogDbContext, TEntity>
        where TEntity : EntityBase
    {
        public BlogEfRepository(BlogDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}