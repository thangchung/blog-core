using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BlogCore.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Infrastructure.Data
{
    public class BlogCoreDbContext : IdentityDbContext<AppUser>
    {
        public BlogCoreDbContext(DbContextOptions<BlogCoreDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typeToRegisters = new List<Type>();
            typeToRegisters.AddRange(typeof(EntityBase).GetTypeInfo().Assembly.DefinedTypes.Select(t => t.AsType()));
            var entityTypes = typeToRegisters.Where(x => !x.GetTypeInfo().IsAbstract
                                                         && x.GetTypeInfo().BaseType == typeof(EntityBase));

            // temporary to concanate with s at the end, but need to have a way to translate it to a plural noun
            foreach (var type in entityTypes)
                modelBuilder.Entity(type).ToTable($"{type.Name}s", "blog");

            base.OnModelCreating(modelBuilder);
        }
    }
}