using BlogCore.Infrastructure.EfCore;
using BlogCore.PostContext.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BlogCore.PostContext.Infrastructure
{
    public class PostDbContext : DbContext
    {
        public PostDbContext(DbContextOptions<PostDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityTypes = new List<Type>
            {
                typeof(Post),
                typeof(Comment),
                typeof(Tag)
            };

            var valueTypes = new List<Type>
            {
                typeof(BlogId),
                typeof(AuthorId)
            };

            base.OnModelCreating(modelBuilder.RegisterTypes(entityTypes, valueTypes, "post", "post"));
        }
    }
}