using AuthAPI.DataBase;
using AuthAPI.Models;

namespace AuthAPI.Repositories
{
    public class SessionRepository
    {
        private readonly AppDbContext _dbContext;

        public SessionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task Add(Session session)
        {
            _dbContext.Session.Add(session);
            await _dbContext.SaveChangesAsync();
        }
    }
}