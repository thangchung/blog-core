using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Infrastructure.EfCore
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder RegisterTypes(this ModelBuilder modelBuilder, IEnumerable<Type> entities,
            IEnumerable<Type> valueObjects, string entitySchema, string valueObjectSchema = "shared")
        {
            var entityTypes = new List<Type>();
            entityTypes.AddRange(entities);

            // temporary to concanate with s at the end, but need to have a way to translate it to a plural noun
            foreach (var type in entityTypes)
                modelBuilder.Entity(type).ToTable($"{type.Name}s", entitySchema);

            var valueTypes = new List<Type>();
            valueTypes.AddRange(valueObjects);

            // temporary to concanate with s at the end, but need to have a way to translate it to a plural noun
            foreach (var type in valueTypes)
                modelBuilder.Entity(type).ToTable($"{type.Name}s", valueObjectSchema);

            return modelBuilder;
        }
    }
}