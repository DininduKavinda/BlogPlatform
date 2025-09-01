using TaskManagement.Models;

namespace TaskManagement.Repositories.Interfaces
{
    public interface IAuthRepository : IRepository<User>
    {
         Task<User> GetByEmailAsync(string email);
         Task<User> GetByUsernameAsync(string username);
         Task<User> GetUserWithRoleAsync(int id);
        Task UpdateLastLoginAsync(int id);
    }
}