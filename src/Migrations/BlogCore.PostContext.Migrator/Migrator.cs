using BlogCore.PostContext.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BlogCore.PostContext.Migrator
{
    public class Migrator
    {
        public static async Task Run(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<PostDbContext>();
                if (context.Database.GetPendingMigrations() != null)
                    context.Database.Migrate();

                await PostContextSeeder.Seed(context);
            }
        }
    }
}
