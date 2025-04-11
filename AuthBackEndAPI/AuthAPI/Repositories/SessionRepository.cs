using AuthAPI.DataBase;
using AuthAPI.Models;

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
            throw new NotImplementedException();
        }

        public override async Task<Session> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}