using System;
using System.Threading.Tasks;
using BlogCore.Core.Blogs;
using BlogCore.Infrastructure.Data;

namespace BlogCore.Infrastructure.MigrationConsole.SeedData
{
    public static class BlogSeeder
    {
        public static async Task Seed(BlogCoreDbContext dbContext)
        {
            for (var i = 1; i <= 1; i++)
                await dbContext.Set<Blog>().AddAsync(new Blog
                {
                    Id = new Guid("5b1fa7c2-f814-47f2-a2f3-03866f978c49"),
                    Title = $"Blog {i}",
                    Description = $"Blog {i}'s description",
                    Image = null,
                    Theme = "default",
                    PostsPerPage = 10,
                    ModerateComments = true,
                    DaysToComment = 10
                });

            await dbContext.SaveChangesAsync();
        }
    }
}