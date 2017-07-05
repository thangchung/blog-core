using System;
using System.Threading.Tasks;
using BlogCore.Blog.Infrastructure;

namespace BlogCore.Blog.Migrator
{
    public static class BlogContextSeeder
    {
        public static async Task Seed(BlogDbContext dbContext)
        {
            for (var i = 1; i <= 1; i++)
                await dbContext.Set<Domain.Blog>().AddAsync(new Domain.Blog
                {
                    Id = new Guid("5b1fa7c2-f814-47f2-a2f3-03866f978c49"),
                    Title = $"Blog {i} - Root",
                    Description = $"Blog {i}'s description",
                    ImageFilePath = null,
                    Theme = "default",
                    BlogSetting = new Domain.BlogSetting(Guid.NewGuid(), 10, 10, true),
                    OwnerEmail = "root@blogcore.com",
                    Posts = new System.Collections.Generic.List<Domain.PostId>
                    {
                        new Domain.PostId(new Guid("f2384450-37af-4e4d-aaab-6c6dcc6c81d7"))
                    }
                });

            await dbContext.Set<Domain.Blog>().AddAsync(new Domain.Blog
            {
                Id = new Guid("34c96712-2cdf-4e79-9e2f-768cb68dd552"),
                Title = "Blog for user1 - Normal User",
                Description = "Blog for user1's description",
                ImageFilePath = null,
                Theme = "default",
                BlogSetting = new Domain.BlogSetting(Guid.NewGuid(), 10, 10, true),
                OwnerEmail = "user1@blogcore.com",
                Posts = new System.Collections.Generic.List<Domain.PostId>
                {
                    new Domain.PostId(new Guid("5ac8dbfa-c258-43db-b0a1-2c1be6160d67"))
                }
            });

            await dbContext.SaveChangesAsync();
        }
    }
}