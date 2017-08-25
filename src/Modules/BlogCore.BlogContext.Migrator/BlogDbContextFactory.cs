using System.Reflection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using BlogCore.BlogContext.Infrastructure;
using BlogCore.Infrastructure.EfCore;

namespace BlogCore.BlogContext.Migrator
{
    public class BlogDbContextFactory : IDbContextFactory<BlogDbContext>
    {
        public BlogDbContext Create(DbContextFactoryOptions options)
        {
            return new BlogDbContext(
                options.BuildDbContext<BlogDbContext>(
                    typeof(BlogDbContextFactory).GetTypeInfo().Assembly).Options);
        }
    }
}