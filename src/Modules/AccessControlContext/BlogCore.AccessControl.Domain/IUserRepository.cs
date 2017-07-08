using System;
using System.Threading.Tasks;

namespace BlogCore.AccessControl.Domain
{
    public interface IUserRepository
    {
        Task<AppUser> GetByIdAsync(Guid id);
    }
}