using BlogCore.AccessControlContext.Core.Domain;
using BlogCore.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace BlogCore.AccessControlContext.Infrastructure
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
            var userSet = _dbContext.Set<AppUser>();
            var user = await userSet.SingleOrDefaultAsync(x => Guid.Parse(x.Id) == id);
            return user;
        }

        public IObservable<AppUser> GetByIdObs(Guid id)
        {
            return _dbContext.Set<AppUser>()
                .AsNoTracking()
                .ToListAsync()
                .ToObservable()
                .Select(x => x.FirstOrDefault(y => y.Id == id.ToString()));
        }

        public Task<AppUser> GetByUserNameAsync(string username)
        {
            return _dbContext.Set<AppUser>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserName == username);
        }

        public IObservable<AppUser> GetByUserNameObs(string username)
        {
            return _dbContext.Set<AppUser>()
                .AsNoTracking()
                .ToListAsync()
                .ToObservable()
                .Select(x => x.FirstOrDefault(y => y.UserName == username));
        }

        public async Task UpdateUserProfile(Guid id, string givenName, string familyName, string bio, string company,
            string location)
        {
            var user = await _dbContext.Set<AppUser>().SingleOrDefaultAsync(x => Guid.Parse(x.Id) == id);
            if (user == null)
                throw new CoreException($"Could not find out UserProfile with id={id}.");

            user.GivenName = givenName;
            user.FamilyName = familyName;
            user.Bio = bio;
            user.Company = company;
            user.Location = location;
            await _dbContext.SaveChangesAsync();
        }
    }
}