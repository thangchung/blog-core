using System;

namespace BlogCore.Core.BlogFeature
{
    public interface IBlogService
    {
        IObservable<Blog> GetBlogs();
    }
}