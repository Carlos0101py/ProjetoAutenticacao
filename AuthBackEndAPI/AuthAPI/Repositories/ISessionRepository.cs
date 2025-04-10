using AuthAPI.Models;

namespace AuthAPI.Repositories
{
    public interface ISessionRepository : IRepository<Session>
    {
        Task Add(Session session);
        Task<IEnumerable<Session>> GetAll();
        Task<Session> GetById(Guid id);
        Task Delete(Session session);
    }
}