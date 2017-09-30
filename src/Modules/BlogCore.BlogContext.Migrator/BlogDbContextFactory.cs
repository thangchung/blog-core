using BlogCore.BlogContext.Infrastructure;
using BlogCore.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

namespace BlogCore.BlogContext.Migrator
{
    public class BlogDbContextFactory : IDesignTimeDbContextFactory<BlogDbContext>
    {
        public BlogDbContext CreateDbContext(string[] args)
        {
            return new BlogDbContext(
                new DbContextOptionsBuilder<BlogDbContext>().BuildSqlServerDbContext(
                    typeof(BlogDbContextFactory).GetTypeInfo().Assembly).Options);
        }
    }
}