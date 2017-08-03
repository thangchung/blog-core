using System;
using System.Threading.Tasks;
using BlogCore.Blog.Domain;
using BlogCore.Blog.Infrastructure;
using BlogCore.Core;
using BlogCore.Core.Helpers;

namespace BlogCore.Blog.Migrator
{
    public static class BlogContextSeeder
    {
        public static async Task Seed(BlogDbContext dbContext)
        {
            var defaultPost = Domain.Blog.CreateInstance(
                    new Guid("34c96712-2cdf-4e79-9e2f-768cb68dd552"),
                    "Blog for thangchung",
                    "thangchung@blogcore.com")
                .ChangeDescription("Blog for thangchung's description")
                .ChangeSetting(new BlogSetting(IdHelper.GenerateId(), 10, 5, true));

            await dbContext.Set<Domain.Blog>().AddAsync(defaultPost);

            for (var i = 1; i <= 1; i++)
            {
                var blog = Domain.Blog.CreateInstance(
                        new Guid("5b1fa7c2-f814-47f2-a2f3-03866f978c49"),
                        $"Blog {i} - Root",
                        "root@blogcore.com")
                    .ChangeDescription($"Blog {i}'s description")
                    .ChangeSetting(new BlogSetting(IdHelper.GenerateId(), 10, 5, true));

                await dbContext.Set<Domain.Blog>().AddAsync(blog);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}