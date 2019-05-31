using System;
using System.Threading.Tasks;

namespace BlogCore.Modules.AccessControlContext.Domain
{
    public interface IUserRepository
    {
        Task<AppUser> GetByIdAsync(Guid id);
        Task<AppUser> GetByUserNameAsync(string username);
        Task UpdateUserProfile(Guid id, string givenName, string familyName, string bio, string company, string location);
        Task<AppUser> GetById(Guid id);
        Task<AppUser> GetByUserNameO(string username);
    }
}