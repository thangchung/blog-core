using BlogCore.Infrastructure.EfCore;
using BlogCore.PostContext.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

namespace BlogCore.Migrator
{
    public class PostDbContextFactory : IDesignTimeDbContextFactory<PostDbContext>
    {
        public PostDbContext CreateDbContext(string[] args)
        {
            return new PostDbContext(
                new DbContextOptionsBuilder<PostDbContext>().BuildSqlServerDbContext(
                    typeof(PostDbContextFactory).GetTypeInfo().Assembly).Options);
        }
    }
}