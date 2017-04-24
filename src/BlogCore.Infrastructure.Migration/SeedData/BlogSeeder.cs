using System.Threading.Tasks;
using BlogCore.Core.BlogFeature;
using BlogCore.Infrastructure.Data;

namespace BlogCore.Infrastructure.MigrationConsole.SeedData
{
    public static class BlogSeeder
    {
        public static async Task Seed(BlogCoreDbContext dbContext)
        {
            for (var i = 1; i <= 100; i++)
            {
                await dbContext.Set<Blog>().AddAsync(new Blog
                {
                    Title = $"Blog {i}",
                    Description = $"Blog {i}'s description",
                    Image = null,
                    Theme = "default",
                    PostsPerPage = 10,
                    ModerateComments = true,
                    DaysToComment = 10
                });
            }
            
            await dbContext.SaveChangesAsync();
        }
    }
}