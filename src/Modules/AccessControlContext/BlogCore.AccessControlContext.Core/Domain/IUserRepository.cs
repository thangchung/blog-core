using System;
using System.Threading.Tasks;

namespace BlogCore.AccessControlContext.Core.Domain
{
    public interface IUserRepository
    {
        Task<AppUser> GetByIdAsync(Guid id);
        Task UpdateUserProfile(Guid id, string givenName, string familyName, string bio, string company, string location);
        IObservable<AppUser> GetByIdStream(Guid id);
    }
}