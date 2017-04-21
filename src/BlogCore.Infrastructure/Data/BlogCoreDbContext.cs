using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BlogCore.Core;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Infrastructure.Data
{
    public class BlogCoreDbContext : DbContext
    {
        public BlogCoreDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typeToRegisters = new List<Type>();
            typeToRegisters.AddRange(typeof(EntityBase).GetTypeInfo().Assembly.DefinedTypes.Select(t => t.AsType()));
            var entityTypes = typeToRegisters.Where(x => !x.GetTypeInfo().IsAbstract
                                                         && x.GetTypeInfo().BaseType == typeof(EntityBase));

            foreach (var type in entityTypes)
                modelBuilder.Entity(type);

            base.OnModelCreating(modelBuilder);
        }
    }
}