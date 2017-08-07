using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Reflection;

namespace BlogCore.Infrastructure.EfCore
{
    public class DbContextHelper
    {
        public static DbContextOptions<TDbContext> BuildDbContextOption<TDbContext>(string connString, Assembly assembly = null)
            where TDbContext : DbContext
        {
            var dbContextOptionBuilder = new DbContextOptionsBuilder<TDbContext>();
            Action<SqlServerDbContextOptionsBuilder> action = null;
            if (assembly != null)
            {
                action = b => b.MigrationsAssembly(assembly.GetName().Name);
            }

            return dbContextOptionBuilder.UseSqlServer(connString, action).Options;
        }

        public static TDbContext BuildDbContext<TDbContext>(string connString, Assembly assembly = null)
            where TDbContext : DbContext
        {
            var options = BuildDbContextOption<TDbContext>(connString, assembly);
            return (TDbContext)Activator.CreateInstance(typeof(TDbContext), options);
        }
    }
}
