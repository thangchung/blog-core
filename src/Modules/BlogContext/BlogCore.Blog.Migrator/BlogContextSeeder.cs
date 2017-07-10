using System;
using System.Threading.Tasks;
using BlogCore.Blog.Domain;
using BlogCore.Blog.Infrastructure;
using BlogCore.Core;

namespace BlogCore.Blog.Migrator
{
    public static class BlogContextSeeder
    {
        public static async Task Seed(BlogDbContext dbContext)
        {
            var defaultPost = new Domain.Blog(
                    new Guid("34c96712-2cdf-4e79-9e2f-768cb68dd552"),
                    "Blog for thangchung",
                    "thangchung@blogcore.com")
                .UpdateDescription("Blog for thangchung's description")
                .UpdateBlogSetting(new BlogSetting(IdHelper.GenerateId(), 10, 10, true))
                .AddBlogPost(new PostId(new Guid("5ac8dbfa-c258-43db-b0a1-2c1be6160d67")));

            await dbContext.Set<Domain.Blog>().AddAsync(defaultPost);

            for (var i = 1; i <= 1; i++)
            {
                var blog = new Domain.Blog(
                        new Guid("5b1fa7c2-f814-47f2-a2f3-03866f978c49"),
                        $"Blog {i} - Root",
                        "root@blogcore.com")
                    .UpdateDescription($"Blog {i}'s description")
                    .UpdateBlogSetting(new BlogSetting(IdHelper.GenerateId(), 10, 10, true))
                    .AddBlogPost(new PostId(new Guid("f2384450-37af-4e4d-aaab-6c6dcc6c81d7")));

                await dbContext.Set<Domain.Blog>().AddAsync(blog);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}