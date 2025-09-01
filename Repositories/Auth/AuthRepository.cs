using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.Models;
using TaskManagement.Repositories.Interfaces;

namespace TaskManagement.Repositories.Auth
{
    public class AuthRepository : Repository<User>, IAuthRepository
    {
        public AuthRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<User> GetByEmailAsync(string email)
        {

            return await _dbSet.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);

        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbSet.Include(u => u.Role).FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetUserWithRoleAsync(int id)
        {
            return await _dbSet.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateLastLoginAsync(int id)
        {
            var user = await _dbSet.FindAsync(id);
            if (user != null)
            {
                user.LastLoginAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }

}