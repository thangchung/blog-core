using System.Reflection;
using BlogCore.Infrastructure.EfCore;
using BlogCore.Post.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BlogCore.Post.Migrator
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