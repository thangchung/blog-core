using BlogCore.Core;
using BlogCore.Infrastructure.EfCore;

namespace BlogCore.PostContext.Infrastructure
{
    public class BlogEfRepository<TEntity> : EfRepository<PostDbContext, TEntity>
        where TEntity : EntityBase
    {
        public BlogEfRepository(PostDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}