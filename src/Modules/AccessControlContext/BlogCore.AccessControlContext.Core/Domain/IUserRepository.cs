using System;
using System.Threading.Tasks;

namespace BlogCore.AccessControlContext.Core.Domain
{
    public interface IUserRepository
    {
        Task<AppUser> GetByIdAsync(Guid id);
        Task<AppUser> GetByUserNameAsync(string username);
        Task UpdateUserProfile(Guid id, string givenName, string familyName, string bio, string company, string location);
        IObservable<AppUser> GetByIdObs(Guid id);
        IObservable<AppUser> GetByUserNameObs(string username);
    }
}