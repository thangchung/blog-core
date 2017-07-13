using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Post.Infrastructure
{
    public static class DbSetExtensions
    {
        public static async Task<IEnumerable<Domain.Post>> GetPostByPage(
            this DbSet<Domain.Post> postSet, 
            Guid blogId,
            int page)
        {
            return await postSet
                .Include(x => x.Comments)
                .Include(x => x.Author)
                .Include(x => x.Blog)
                .Include(x => x.Tags)
                .Where(x => x.Blog.Id == blogId)
                .OrderBy(x => x.CreatedAt)
                .Skip(page)
                .Take(10)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}