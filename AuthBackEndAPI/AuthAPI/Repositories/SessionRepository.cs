using AuthAPI.DataBase;
using AuthAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Repositories
{
    public class SessionRepository : RepositoryBase<Session>, ISessionRepository
    {
        public SessionRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task Add(Session session)
        {
            _context.Session.Add(session);
            await _context.SaveChangesAsync();
        }

        public override async Task Delete(Session session)
        {
            _context.Session.Remove(session);
            await _context.SaveChangesAsync();
        }

        public override async Task<IEnumerable<Session>> GetAll()
        {
            return await _context.Session.ToListAsync();
        }

        public override async Task<Session> GetById(Guid id)
        {
            return await _context.Session.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}