using System;
using System.Collections.Generic;
using BlogCore.Infrastructure.EfCore;
using BlogCore.Post.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Post.Infrastructure
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
                typeof(Domain.Post),
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