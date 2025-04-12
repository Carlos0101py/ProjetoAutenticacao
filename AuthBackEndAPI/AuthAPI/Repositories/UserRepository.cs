using AuthAPI.DataBase;
using AuthAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task Add(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }

        public override async Task Delete(User user)
        {
            _context.User.Remove(user);
            await _context.SaveChangesAsync();

        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            return await _context.User.ToListAsync();
        }

        public override async Task<User> GetById(Guid id)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByUserName(string userName)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task Change()
        {
            await _context.SaveChangesAsync();
        }
    }
}