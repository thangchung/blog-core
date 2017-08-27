using BlogCore.Infrastructure.EfCore;
using BlogCore.PostContext.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;

namespace BlogCore.PostContext.Migrator
{
    public class PostDbContextFactory : IDbContextFactory<PostDbContext>
    {
        public PostDbContext Create(DbContextFactoryOptions options)
        {
            return new PostDbContext(
                options.BuildDbContext<PostDbContext>(
                    typeof(PostDbContextFactory).GetTypeInfo().Assembly).Options);
        }
    }
}