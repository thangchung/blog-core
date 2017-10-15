using System;
using System.Collections.Generic;
using BlogCore.BlogContext.Core.Domain;
using BlogCore.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.BlogContext.Infrastructure
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityTypes = new List<Type>
            {
                typeof(Core.Domain.Blog)
            };

            var valueTypes = new List<Type>
            {
                typeof(BlogSetting)
            };

            base.OnModelCreating(modelBuilder.RegisterTypes(entityTypes, valueTypes, "blog", "blog"));
        }
    }
}