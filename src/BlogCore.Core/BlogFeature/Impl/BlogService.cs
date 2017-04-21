using System;
using System.Reactive.Linq;

namespace BlogCore.Core.BlogFeature.Impl
{
    public class BlogService : IBlogService
    {
        private readonly IRepository<Blog> _blogRepo;
        private readonly IDomainEventDispatcher _dispatcher;

        public BlogService(IRepository<Blog> blogRepo, IDomainEventDispatcher dispatcher)
        {
            _blogRepo = blogRepo;
            _dispatcher = dispatcher;
        }

        public IObservable<Blog> GetBlogs()
        {
            return _blogRepo.List().ToObservable();
        }
    }
}