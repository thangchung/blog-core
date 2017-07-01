using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogCore.Core.PostContext;
using BlogCore.Infrastructure.EfCore;

namespace BlogCore.Migrator.SeedData
{
    public static class PostSeeder
    {
        public static async Task Seed(BlogCoreDbContext dbContext)
        {
            var blogId = new BlogId(new Guid("5b1fa7c2-f814-47f2-a2f3-03866f978c49"));

            for (var i = 1; i <= 1000; i++)
                await dbContext.Set<Post>().AddAsync(new Post
                {
                    Id = Guid.NewGuid(),
                    BlogId = blogId,
                    Title = $"Post {i}",
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                            Id = Guid.NewGuid()
                        }
                    }
                });

            await dbContext.SaveChangesAsync();
        }
    }
}