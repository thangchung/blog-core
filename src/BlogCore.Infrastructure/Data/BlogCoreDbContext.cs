using System.Linq;
using BlogCore.Core;
using BlogCore.Core.BlogFeature;
using BlogCore.Core.PostFeature;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Infrastructure.Data
{
    public class BlogCoreDbContext : DbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;

        public BlogCoreDbContext(DbContextOptions options, IDomainEventDispatcher dispatcher)
            : base(options)
        {
            _dispatcher = dispatcher;
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public override int SaveChanges()
        {
            var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            foreach (var entity in entitiesWithEvents)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();
                foreach (var domainEvent in events)
                {
                    _dispatcher.Dispatch(domainEvent);
                }
            }
            return base.SaveChanges();
        }
    }
}