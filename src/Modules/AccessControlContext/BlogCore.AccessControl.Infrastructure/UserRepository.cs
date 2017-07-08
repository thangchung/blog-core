using System;
using System.Threading.Tasks;
using BlogCore.AccessControl.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.AccessControl.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityServerDbContext _dbContext;

        public UserRepository(IdentityServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AppUser> GetByIdAsync(Guid id)
        {
            var userSet =  _dbContext.Set<AppUser>();
            var user = await userSet.SingleOrDefaultAsync(x => Guid.Parse(x.Id) == id);
            return user;
        }
    }
}