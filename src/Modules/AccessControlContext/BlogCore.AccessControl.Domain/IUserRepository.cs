using System;
using System.Threading.Tasks;

namespace BlogCore.AccessControl.Domain
{
    public interface IUserRepository
    {
        Task<AppUser> GetByIdAsync(Guid id);

        Task UpdateUserProfile(Guid id, string givenName, string familyName, string bio, string company,
            string location);
    }
}