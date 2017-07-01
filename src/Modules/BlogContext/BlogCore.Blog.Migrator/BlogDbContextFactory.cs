using System.Reflection;
using BlogCore.Blog.Infrastructure;
using BlogCore.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BlogCore.Blog.Migrator
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